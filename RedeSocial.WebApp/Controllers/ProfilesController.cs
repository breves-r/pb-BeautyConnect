using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Services;

namespace RedeSocial.WebApp.Controllers
{
    [Authorize]
    public class ProfilesController : Controller
    {
        private readonly ProfileService _service;

        private const string connectionString = "DefaultEndpointsProtocol=https;AccountName=mariamafrastorage;AccountKey=/vR7bYN7PWaz+06lScJ5tZeXADYNTIFfurNas3AbSD7DFim2085PmZU7sF8Dx6NrML3+3Wijcs2z+AStwRdMgA==;EndpointSuffix=core.windows.net";
        private const string containerName = "imagens";

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
        public IActionResult Create(Profile profile, IFormFile Foto)
        {
            if (ModelState.IsValid)
            {
                profile.Foto = UploadImage(Foto);
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
        public IActionResult Edit(Guid id, Profile profile, IFormFile Foto)
        {
            if (id != profile.IdProfile)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                profile.Foto = UploadImage(Foto);
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
    }
}
