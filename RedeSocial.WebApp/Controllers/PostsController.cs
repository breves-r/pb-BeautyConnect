using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Services;
using System.Security.Claims;

namespace RedeSocial.WebApp.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly PostService _service;
        private readonly ProfileService _profileService;

        public PostsController(PostService service, ProfileService profileService)
        {
            _service = service;
            _profileService = profileService;
        }

        // GET: Posts
        public IActionResult Index()
        {
            return View(_service.ConsultarPosts());
        }

        // GET: Posts/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _service.PostVazio())
            {
                return NotFound();
            }

            var post = _service.ConsultarPost(id.Value);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                post.Profile = GetProfile();
                post.Profile.Posts.Add(post);
                _service.CriarPost(post);
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _service.PostVazio())
            {
                return NotFound();
            }

            var post = _service.ConsultarPost(id.Value);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool result = _service.AlterarPost(post);

                if (!result)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _service.PostVazio())
            {
                return NotFound();
            }

            var post = _service.ConsultarPost(id.Value);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if ( _service.PostVazio())
            {
                return Problem("Entity set 'RedeSocialDbContext.Posts'  is null.");
            }
            var post = _service.ConsultarPost(id);
            if (post != null)
            {
                _service.DeletarPost(post);
            }

            return RedirectToAction(nameof(Index));
        }

        private Profile GetProfile()
        {
            var id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return _profileService.ConsultarProfile(id);
        }
    }
}
