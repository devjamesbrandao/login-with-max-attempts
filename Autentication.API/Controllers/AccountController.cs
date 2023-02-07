using Autentication.API.Configuration;
using Autentication.Core.DTO;
using Autentication.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Autentication.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v1/api/[controller]")]
    public class AccountController : MainController
    {
        private readonly IAccountService _accountService;

        public AccountController(
            IAccountService accountService,
            INotificator notificator
        ) : base(notificator)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Checks if the user is logged in
        /// </summary>
        /// <remarks>
        /// Requisition example:
        /// 
        ///     [GET] v1/api/Account/account
        /// </remarks>
        /// <response code="200">
        /// User is logged in.
        /// </response>   
        /// <response code="401">
        /// User is not logged in.
        /// </response>   
        [HttpGet("account")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public ActionResult<string> GetUser() => $"Hello, {User!.Identity!.Name}";


        /// <summary>
        /// Sign up in the application
        /// </summary>
        /// <remarks>
        /// Requisition example:
        /// 
        ///     [POST] v1/api/Account/sign-up
        ///     {        
        ///       "Username": "Thanos",
        ///       "Password": "jewels"
        ///     }
        /// </remarks>
        /// <param name="request"></param> 
        /// <response code="400">
        /// Possible errors: "Username can not be empty."; "Password can not be empty."; "Username 'Dumbledore' is already in use.";
        /// </response>    
        /// <response code="204">
        /// Success sign up
        /// </response>   
        [HttpPost("sign-up")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignUp([FromBody] UserModelView request)
        {
            await _accountService.SignUp(request.Username, request.Password);
            
            return CustomResponse();
        }


        /// <summary>
        /// Sign in in the application
        /// </summary>
        /// <remarks>
        /// Requisition example:
        /// 
        ///     [POST] v1/api/Account/sign-in
        ///     {        
        ///       "Username": "Thanos",
        ///       "Password": "jewels"
        ///     }
        /// </remarks>
        /// <param name="request"></param> 
        /// <response code="400">
        /// Possible errors: "Invalid username or password";
        /// </response>    
        /// <response code="200">
        /// Success sign in 
        /// </response>   
        /// [HttpPost("sign-in")]
        [AllowAnonymous]
        [HttpPost("sign-in")]
        [ProducesResponseType(typeof(JsonWebToken), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<JsonWebToken>> SignIn([FromBody] UserModelView request)
        {
            var token = await _accountService.SignIn(request.Username, request.Password);

            return CustomResponse(token);
        }
    }
}