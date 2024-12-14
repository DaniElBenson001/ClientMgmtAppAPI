using ClientMgmtAppAPI.Common.Constants;
using ClientMgmtAppAPI.Models.DtoModels;
using ClientMgmtAppAPI.Services.Data;
using ClientMgmtAppAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ClientMgmtAppAPI.Services.Services
{
    public class SearchService : ISearchService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;
        private readonly ILogger<SearchService> _logger;

        public SearchService(IHttpContextAccessor httpContextAccessor, DataContext context, ILogger<SearchService> logger)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;

        }

        public async Task<DataResponse<SearchResultDTO<List<ClientDTO>>>> SearchClient(SearchRequestDTO request)
        {
            DataResponse<SearchResultDTO<List<ClientDTO>>> searchResponse = new()
            {
                Data = new SearchResultDTO<List<ClientDTO>>()
            };

            int userID;
            var userHttpAccessor = _httpContextAccessor.HttpContext;


            try
            {
                if(userHttpAccessor == null)
                {
                    searchResponse.Status = false;
                    searchResponse.StatusMessage = Messages.ErrorMessage.UserNotFound;
                    _logger.LogWarning($"User Context is Null; {Messages.ErrorMessage.UserNotFound}");
                    return searchResponse;
                }

                userID = Convert.ToInt32(userHttpAccessor!.User.FindFirstValue(ClaimTypes.NameIdentifier));

                var userExists = await _context.Users.AnyAsync(c => c.Id == userID);

                if (userExists)
                {
                    var query = _context.Clients.Where(c => c.UserId == userID && EF.Functions.Like(c.CompanyName, $"%{request.Query}%"));

                    var totalCount = await query.CountAsync();

                    var  clients = await query
                        .Skip((request.PageNumber -1) * request.PageSize)
                        .Take(request.PageSize)
                        .ToListAsync();

                    List<ClientDTO> getSearchResults = new();

                    foreach(var result in clients)
                    {
                        getSearchResults.Add(new ClientDTO
                        {
                            CompanyName = result.CompanyName,
                            ContactName = result.ContactName,
                            Email = result.Email,
                            PhoneNumber = result.PhoneNumber
                        });
                    }

                    var searchResult = new SearchResultDTO<List<ClientDTO>>()
                    {
                        Totalcount = totalCount,
                        CurrentPage = request.PageNumber,
                        PageSize = request.PageSize,
                        Results = getSearchResults
                    };

                    searchResponse.Status = true;
                    searchResponse.StatusMessage = Messages.SuccessMessage.SearchSuccess;
                    searchResponse.Data = searchResult;
                }
                return searchResponse;
            }
            catch (Exception ex) when (ex is SqlException || ex is DbUpdateException)
            {
                searchResponse.Status = false;
                searchResponse.StatusMessage = Messages.ErrorMessage.BaseError;
                _logger.LogError($"{ex.GetType().Name}: {ex.Message} ||| {ex.StackTrace}");
                return searchResponse;
            }
        }
    }
}
