using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Services;
using System.Security.Claims;

namespace RedeSocial.WebApp.Controllers
{
    [Authorize]
    public class FriendshipController : Controller
    {
        private readonly FriendshipService _service;

        public FriendshipController(FriendshipService service)
        {
            _service = service;
        }


        //GET Friendhsips
        public IActionResult Index()
        {
            return View(_service.GetFriends(GetUserId()));
        }

        //POST: Friendships/Create/:id
        [HttpPost]
        public IActionResult Create(Guid id)
        {
            Console.WriteLine(GetUserId());
            Console.WriteLine(id);
            bool friends = _service.CriarAmizade(GetUserId(), id);

            if(friends)
            {
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

            private Guid GetUserId()
        {
            // Busca na tabela AspNetUser o primeiro campo
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
