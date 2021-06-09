using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Write.Inputs;
using Write.Interfaces;

namespace Write.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository _repository;

        public ArticleController(IArticleRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult> AddArticleAsync([FromBody] ArticleInput input) =>
            Ok(new { Message = await _repository.AddArticleAsync(input) });

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateArticleAsync([FromRoute] int id, [FromBody] ArticleInput input) =>
            Ok(new { Message = await _repository.UpdateArticleAsync(id, input) });

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveArticleAsync([FromRoute] int id) =>
            Ok(new { Message = await _repository.RemoveArticleAsync(id) });
    }
}