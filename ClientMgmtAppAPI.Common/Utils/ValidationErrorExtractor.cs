using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMgmtAppAPI.Common.Utils
{
    public class ValidationErrorExtractor
    {
        public class ValidationErrorDTO
        {
            public string Field { get; set; } = string.Empty;
            public string ErrorMessage { get; set; } = string.Empty;
        }
        public static List<ValidationErrorDTO> ExtractValidationError(ModelStateDictionary modelState)
        {
            return modelState
                .SelectMany(kvp => kvp.Value!.Errors.Select(error => new ValidationErrorDTO
                {
                    Field = kvp.Key,
                    ErrorMessage = error.ErrorMessage,
                }))
                .ToList();
        }
    }
}
