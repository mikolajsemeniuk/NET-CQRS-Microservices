using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Read.Data;
using Read.Interfaces;
using Read.Payloads;

namespace Read.Services
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly DataContext _context;

        public ArticleRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ArticlePayload>> GetArticlesAsync() =>
            await _context.Articles
                .Select(article => new ArticlePayload
                {
                    ArticleId = article.ArticleId,
                    Title = article.Title,
                    Description = article.Description,
                    ReadId = article.ReadId,
                    CreatedAt = article.CreatedAt,
                    UpdatedAt = article.UpdatedAt
                })
                .ToListAsync();

        public async Task<ArticlePayload> GetArticleAsync(int id) =>
            await _context.Articles
                .Where(article => article.ArticleId == id)
                .Select(article => new ArticlePayload
                {
                    ArticleId = article.ArticleId,
                    Title = article.Title,
                    Description = article.Description,
                    ReadId = article.ReadId,
                    CreatedAt = article.CreatedAt,
                    UpdatedAt = article.UpdatedAt
                })
                .SingleAsync();
    }
}