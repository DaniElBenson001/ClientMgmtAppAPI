using ClientMgmtAppAPI.Models.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMgmtAppAPI.Services.IServices
{
    public interface ISearchService
    {
        Task<DataResponse<SearchResultDTO<List<ClientDTO>>>> SearchClient(SearchRequestDTO request);
    }
}
