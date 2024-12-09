using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMgmtAppAPI.Models.DtoModels
{
    public class LoginTokenDTO
    {
        public string Email { get; set; } = string.Empty;
        public string VerificationToken { get; set; } = string.Empty;
    }
}
