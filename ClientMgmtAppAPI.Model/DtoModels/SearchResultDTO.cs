using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMgmtAppAPI.Models.DtoModels
{
    public class SearchResultDTO<T>
    {
        public int Totalcount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public T? Results { get; set; }
    }
}
