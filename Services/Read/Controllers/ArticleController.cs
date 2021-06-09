using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Read.Interfaces;
using Read.Payloads;

namespace Read.Controllers
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticlePayload>>> GetArticlesAsync() =>
            Ok(await _repository.GetArticlesAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ArticlePayload>>> GetArticleAsync(int id) =>
            Ok(await _repository.GetArticleAsync(id));
    }
}