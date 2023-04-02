using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Services;
using System.Security.Claims;

namespace RedeSocial.WebApp.Controllers
{
    [Authorize]
    public class ProfileDetailsController : Controller
    {
        private readonly ProfileDetailsService _service;

        public ProfileDetailsController(ProfileDetailsService service)
        {
            _service = service;
        }

        // GET: ProfileDetails
        public IActionResult Index(Guid profileId)
        {
            Guid userId = GetUserId();
            ProfileDetails details = _service.ConsultarProfileDetails(profileId);

            if(details == null && profileId == userId)
            {
                return RedirectToAction("Create");
            }
            return View(details);
        }

        //GET: ProfileDetails/Create
        public IActionResult Create()
        {
            Guid userId = GetUserId();

            ProfileDetails details = _service.ConsultarProfileDetails(userId);

            if (details == null)
            {
                return View();
            }

            return RedirectToAction("Index");
        }

        //POST: ProfileDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProfileDetails details)
        {
            Guid userId = GetUserId();
            _service.CriarProfileDetails(details, userId);
            return RedirectToAction(nameof(Index), new { profileId = details.ProfileId });
        }

        //GET: ProfileDetails/Edit/:id
        public IActionResult Edit()
        {
            Guid userId = GetUserId();
            if(_service.ProfileDetailsVazio(userId))
                return NotFound();

            ProfileDetails details = _service.ConsultarProfileDetails(userId);
            if(details == null)
                return NotFound();

            return View(details);
        }

        //POST: ProfileDetails/Edit/:id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProfileDetails details)
        {
            if (id != details.Id)
                return NotFound();

            bool result = _service.AlterarProfileDetails(details);

            if(!result)
                return NotFound();
            return RedirectToAction(nameof(Index), new { profileId = details.ProfileId });
        }

        private Guid GetUserId()
        {
            // Busca na tabela AspNetUser o primeiro campo
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
