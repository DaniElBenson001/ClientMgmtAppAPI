using ClientMgmtAppAPI.Common.Constants;
using ClientMgmtAppAPI.Common.Utils;
using ClientMgmtAppAPI.Models.DtoModels;
using ClientMgmtAppAPI.Services.IServices;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ClientMgmtAppAPI.Controllers
{
    [ApiController]
    [Route("/")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost(ApiRoutes.Version + ApiRoutes.Users.Base + ApiRoutes.Users.Register)]
        public async Task<IActionResult> CreateUser(CreateUserDTO request, [FromServices] IValidator<CreateUserDTO> validator)
        {
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                var modelStateDictionary = new ModelStateDictionary();

                foreach (var failure in validationResult.Errors)
                {
                    modelStateDictionary.AddModelError(
                        failure.PropertyName,
                        failure.ErrorMessage);
                }

                var errors = ValidationErrorExtractor.ExtractValidationError(modelStateDictionary);
                var errorResponse = new DataResponse<object>
                {
                    Status = false,
                    StatusMessage = Messages.ErrorMessage.ValidationError,
                    ValidationErrors = errors
                };

                return BadRequest(errorResponse);
            }

            var res = await _userService.CreateUser(request);
            return Ok(res);
        }

        [HttpGet(ApiRoutes.Version + ApiRoutes.Users.Base), Authorize]
        public async Task<IActionResult> GetUserInfo()
        {
            var res = await _userService.GetUserInfo();
            return Ok(res);
        }
    }
}
