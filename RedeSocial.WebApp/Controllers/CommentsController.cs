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

        /*    // GET: Comments/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null || _context.Comments == null)
                {
                    return NotFound();
                }

                var comment = await _context.Comments.FindAsync(id);
                if (comment == null)
                {
                    return NotFound();
                }
                return View(comment);
            }
    
            // POST: Comments/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao")] Comment comment)
            {
                if (id != comment.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(comment);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CommentExists(comment.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(comment);
            }

            // GET: Comments/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null || _context.Comments == null)
                {
                    return NotFound();
                }

                var comment = await _context.Comments
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (comment == null)
                {
                    return NotFound();
                }

                return View(comment);
            }

            // POST: Comments/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                //
            }
       */
        private Guid GetUserId()
        {
            // Busca na tabela AspNetUser o primeiro campo
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
