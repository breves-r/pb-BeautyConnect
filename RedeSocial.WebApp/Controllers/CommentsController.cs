using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Services;
using RedeSocial.Infra.Context;

namespace RedeSocial.WebApp.Controllers
{
    public class CommentsController : Controller
    {
        private readonly CommentService _service;

        public CommentsController(CommentService service)
        {
            _service = service;
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int postId, Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.ProfileId = GetUserId();
                comment.PostId = postId;
                _service.CriarComment(comment);

                return RedirectToAction("Details", "Posts", new { id = postId });
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            var comment = _service.ConsultarComment(id);
            var postId = comment.PostId;
            if (comment != null)
            {
                _service.DeletarComment(comment);
            }

            return RedirectToAction("Details", "Posts", new { id = postId });
        }
       
        private Guid GetUserId()
        {
            // Busca na tabela AspNetUser o primeiro campo
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
