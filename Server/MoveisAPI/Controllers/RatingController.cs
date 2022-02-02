using AutoMapper;
using DatabaseP.models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoveisAPI.DTOs.Rating;
using ServicesP.Implementation.Interface;

namespace MoveisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public RatingController(IRatingService ratingService, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _ratingService = ratingService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post([FromBody] RatingDTO ratingDTO)
        { 
            //maybe method in service ??
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value;
            var user = await _userManager.FindByEmailAsync(email);
            var userId = user.Id;
            //maybe method in service ??

            var rating = _mapper.Map<Rating>(ratingDTO);
            var currentRate = await _ratingService.getRatingById(userId, rating); //ovo je moglo direktno dole da se zove iz kontrolera null
            await _ratingService.addNewRating(userId, rating, currentRate);
            return NoContent();
        }
    }
}
