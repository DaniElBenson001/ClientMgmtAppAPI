using ClientMgmtAppAPI.Common.Constants;
using ClientMgmtAppAPI.Models.DtoModels;
using ClientMgmtAppAPI.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientMgmtAppAPI.Controllers
{
    [Route("/")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet(ApiRoutes.Version + ApiRoutes.Search.Base + ApiRoutes.Search.SearchClients), Authorize]
        public async Task<IActionResult> SearchClients([FromQuery] SearchRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Query))
            {
                return BadRequest(Messages.ErrorMessage.NullOrNoSearch);
            }

            var res = await _searchService.SearchClient(request);
            return Ok(res);
        }
    }
}
