using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Services;

namespace RedeSocial.WebApp.Controllers
{
    [Authorize]
    public class ProfilesController : Controller
    {
        private readonly ProfileService _service;

        public ProfilesController(ProfileService serice)
        {
            _service = serice;
        }

        public IActionResult List()
        {
            return View(_service.ConsultarProfiles());
        }

        // GET: Profiles
        public IActionResult Index()
        {
            Guid userId = GetUserId();
            Profile profile = _service.ConsultarProfile(userId);

            if (profile == null)
            {
                return RedirectToAction("Create");
            }
            return RedirectToAction("Details");
        }

        // GET: Profiles/Details/5
        public IActionResult Details(Guid? id)
        {
            if (_service.ProfileVazio())
            {
                return NotFound();
            }

            if (id == null)
            {
                id = GetUserId();
            }

            var profile = _service.ConsultarProfile(id.Value);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // GET: Profiles/Create
        public IActionResult Create()
        {
            Guid userId = GetUserId();
            Profile profile = _service.ConsultarProfile(userId);

            if (profile == null)
            {
                return View();
            }

            return RedirectToAction("Details");
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Profile profile)
        {
            if (ModelState.IsValid)
            {
                profile.IdProfile = GetUserId();
                _service.CriarProfile(profile);
                return RedirectToAction(nameof(Index));
            }
            return View(profile);
        }

        // GET: Profiles/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null || _service.ProfileVazio())
            {
                return NotFound();
            }

            var profile = _service.ConsultarProfile(id.Value);
            if (profile == null)
            {
                return NotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, Profile profile)
        {
            if (id != profile.IdProfile)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                bool result = _service.AlterarProfile(profile);

                if (!result)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(profile);
        }

        // GET: Profiles/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null || _service.ProfileVazio())
            {
                return NotFound();
            }

            var profile = _service.ConsultarProfile(id.Value);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            if (_service.ProfileVazio())
            {
                return Problem("Entity set 'RedeSocialDbContext.Profiles'  is null.");
            }
            var profile = _service.ConsultarProfile(id);
            if (profile != null)
            {
                _service.ExcluirProfile(profile);
            }

            return RedirectToAction(nameof(Index));
        }

        private Guid GetUserId()
        {
            // Busca na tabela AspNetUser o primeiro campo
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
