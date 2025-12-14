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
        [HttpGet("getHomeData/{userId}/{LanId}")]
        [ProducesResponseType(typeof(homePageDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetHomePageData(string userId,int LanId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return BadRequest(new {Message = "UserId is required." });

            if (LanId<=0)
                return BadRequest(new { Message = "LanId is Invalid." });

            var result = await _homeService.GetHomePageDataAsync(userId,LanId ,cancellationToken);

            if (result is null)
                return NotFound(new {Message = $"User with Id '{userId}' was not found." });

            return Ok(result);
        }

        #endregion



    }
}
