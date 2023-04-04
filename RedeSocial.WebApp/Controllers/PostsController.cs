using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
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
        private readonly CommentService _commentService;

        private const string connectionString = "DefaultEndpointsProtocol=https;AccountName=mariamafrastorage;AccountKey=/vR7bYN7PWaz+06lScJ5tZeXADYNTIFfurNas3AbSD7DFim2085PmZU7sF8Dx6NrML3+3Wijcs2z+AStwRdMgA==;EndpointSuffix=core.windows.net";
        private const string containerName = "imagens";

        public PostsController(PostService service, ProfileService profileService, CommentService commentService)
        {
            _service = service;
            _profileService = profileService;
            _commentService = commentService;
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

            //Comments section
            ICollection<Comment> comments = _commentService.ConsultarComments(post.Id);

            if(comments != null || comments.Count < 1)
            {
                ViewData["Comments"] = comments;
            }

            ViewData["Comment"] = new Comment();
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
        public IActionResult Create(Post post, IFormFile Imagem)
        {
            if (ModelState.IsValid)
            {
                post.Imagem = UploadImage(Imagem);
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
        public IActionResult Edit(int id, Post post, IFormFile Imagem)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                post.Imagem = UploadImage(Imagem);
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
                ExcluirImagem(post.Imagem);
                _service.DeletarPost(post);
            }

            return RedirectToAction(nameof(Index));
        }

        private Profile GetProfile()
        {
            var id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return _profileService.ConsultarProfile(id);
        }

        private static string UploadImage(IFormFile imageFile)
        {
            var reader = imageFile.OpenReadStream();
            var cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
            var blobClient = cloudStorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(containerName);
            container.CreateIfNotExistsAsync();

            CloudBlockBlob blob = container.GetBlockBlobReference(imageFile.FileName);
            blob.UploadFromStreamAsync(reader);

            System.Threading.Thread.Sleep(1000);

            return blob.Uri.ToString();
        }

        private static void ExcluirImagem(string imagem)
        {
            if (imagem == null) { return; }
            try
            {
                string nomeArquivo = imagem.Split("/" + containerName + "/")[1];
                var blobClient = new BlobClient(connectionString, containerName, nomeArquivo);
                blobClient.Delete();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
