using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Write.Data;
using Write.Inputs;
using Write.Interfaces;

namespace Write.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository _repository;
        // ONLY FOR DEMONSTRATING PURPOSE:
        private readonly DataContext _context;

        public ArticleController(IArticleRepository repository, 
        // ONLY FOR DEMONSTRATING PURPOSE:
        DataContext context
        )
        {
            _repository = repository;
            // ONLY FOR DEMONSTRATING PURPOSE:
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetArticlesAsync() =>
            Ok(await _context.Articles.ToListAsync());

        [HttpPost]
        public async Task<ActionResult> AddArticleAsync([FromBody] ArticleInput input) =>
            Ok(new { Message = await _repository.AddArticleAsync(input) });

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateArticleAsync([FromRoute] Guid id, [FromBody] ArticleInput input) =>
            Ok(new { Message = await _repository.UpdateArticleAsync(id, input) });

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveArticleAsync([FromRoute] Guid id) =>
            Ok(new { Message = await _repository.RemoveArticleAsync(id) });
    }
}