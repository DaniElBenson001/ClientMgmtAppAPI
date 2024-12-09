using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMgmtAppAPI.Models.DtoModels
{
    public class SearchRequestDTO
    {
        public string Query { get; set; } = string.Empty;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
