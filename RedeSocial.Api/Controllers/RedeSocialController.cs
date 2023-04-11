using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Domain.Services;
using System.Runtime.CompilerServices;

namespace RedeSocial.Api.Controllers
{
    [ApiController]
    public class RedeSocialController : ControllerBase
    {
        private readonly ProfileService _profileService;
        private readonly ProfileDetailsService _profileDetailsService;
        private readonly FriendshipService _friendshipService;
        private readonly CommentService _commentService;

        public RedeSocialController(ProfileService profileService, ProfileDetailsService profileDetailsService, FriendshipService friendshipService, CommentService commentService)
        {
            _profileService = profileService;
            _profileDetailsService = profileDetailsService;
            _friendshipService = friendshipService;
            _commentService = commentService;
        }

        [HttpGet("/profile")]
        public IActionResult GetProfiles()
        {
            return Ok(_profileService.ConsultarProfiles());
        }

        [HttpGet("/profileDetails/{id}")]
        public IActionResult GetProfilesDetails(Guid id)
        {
            return Ok(_profileDetailsService.ConsultarProfileDetails(id));
        }

        [HttpGet("/friendship/{id}")]
        public IActionResult GetFriendship(Guid id)
        {
            return Ok(_friendshipService.GetFriends(id));
        }

        [HttpGet("/comments/{id}")]
        public IActionResult GetComments(int id)
        {
            return Ok(_commentService.ConsultarComments(id));
        }
    }
}
