using Microsoft.AspNetCore.Mvc;
using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Services;

namespace RedeSocial.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly PostService _service;
        private readonly ProfileService _profileService;

        public PostsController(PostService service, ProfileService profileService)
        {
            _service = service;
            _profileService = profileService;
        }

        // GET: api/Posts
        [HttpGet]
        public IActionResult GetPosts()
        {
            List<Response> responses = new List<Response>();

            foreach(var post in _service.ConsultarPosts())
            {
                var response = new Response(post.Id, post.Profile.IdProfile, post.CreatedDate, post.Descricao, post.Imagem, post.Produto, post.Categoria, post.Comments);
                responses.Add(response);
            }

            return Ok(responses);
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public IActionResult GetPost(int id)
        {
            if (id == null || _service.PostVazio())
            {
                return NotFound();
            }

            var post = _service.ConsultarPost(id);
            var response = new Response(post.Id, post.Profile.IdProfile, post.CreatedDate, post.Descricao, post.Imagem, post.Produto, post.Categoria, post.Comments);

            return post != null ? Ok(response) : NotFound();
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutPost([FromBody] Post post)
        {
            if (ModelState.IsValid)
            {
                bool result = _service.AlterarPost(post);

                if (!result)
                {
                    return NotFound();
                }

                return Ok();
            }

            return BadRequest(ModelState);
        }

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostPost([FromBody] Response response)
        {
            var profile = _profileService.ConsultarProfile(response.ProfileId);
            var post = new Post(response.Id, profile, response.CreatedDate, response.Descricao, response.Imagem, response.Produto, response.Categoria, response.Comments);

            if (ModelState.IsValid)
            {
                Console.WriteLine("entrei");
                post.Profile.Posts.Add(post);
                _service.CriarPost(post);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public IActionResult DeletePost(int id)
        {
            if (_service.PostVazio())
            {
                return Problem("Entity set 'RedeSocialDbContext.Posts'  is null.");
            }

            var post = _service.ConsultarPost(id);

            if (post != null)
            {
                _service.DeletarPost(post);
                return NoContent();
            }

            return NotFound();
        }
    }

    public class Response
    {
        public Response(int id, Guid profileId, DateTime createdDate, string descricao, string imagem, string produto, string categoria, List<Comment> comments)
        {
            Id = id;
            ProfileId = profileId;
            CreatedDate = createdDate;
            Descricao = descricao;
            Imagem = imagem;
            Produto = produto;
            Categoria = categoria;
            Comments = comments;
        }

        public int Id { get; set; }

        public Guid ProfileId { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Descricao { get; set; }

        public string Imagem { get; set; }

        public string Produto { get; set; }

        public string Categoria { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
