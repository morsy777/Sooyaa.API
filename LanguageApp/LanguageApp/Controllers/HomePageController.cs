using LanguageApp.Application.Bussiness;
using LanguageApp.Application.IBussiness;
using LanguageApp.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LanguageApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomePageController : ControllerBase
    {
        private readonly IhomeService _homeService;

        public HomePageController(IhomeService homeService)
        {
            _homeService = homeService;
        }

        #region Get Home Page Data
        [HttpGet("getHomeData/{userId}")]
        [ProducesResponseType(typeof(homePageDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetHomePageData(string userId,CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return BadRequest(new {Message = "UserId is required." });

            var result = await _homeService.GetHomePageDataAsync(userId, cancellationToken);

            if (result is null)
                return NotFound(new {Message = $"User with Id '{userId}' was not found." });

            return Ok(result);
        }

        #endregion



    }
}
