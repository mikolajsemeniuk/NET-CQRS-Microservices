using System;
using System.Threading.Tasks;
using Events;
using MassTransit;
using Write.Data;
using Write.Inputs;
using Write.Interfaces;
using Write.Models;

namespace Write.Services
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly DataContext _context;
        private readonly IPublishEndpoint _endpoint;

        public ArticleRepository(DataContext context, IPublishEndpoint endpoint)
        {
            _context = context;
            _endpoint = endpoint;
        }

        public async Task<string> AddArticleAsync(ArticleInput input)
        {
            var article = new Article
            {
                Title = input.title,
                Description = input.description
            };

            _context.Articles.Add(article);

            if (await _context.SaveChangesAsync() < 1)
                throw new Exception("problem has occurred");

            await _endpoint.Publish(new AddArticleEvent(article.ArticleId, article.Title, article.Description));

            return "Article Added";
        }

        public async Task<string> UpdateArticleAsync(int id, ArticleInput input)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
                throw new Exception("article with this id does not exist");

            if (article.Title == input.title && article.Description == input.description)
                return "Article Updated";

            article.Title = input.title;
            article.Description = input.description;

            if (await _context.SaveChangesAsync() < 1)
                throw new Exception("problem has occurred");

            await _endpoint.Publish(new UpdateArticleEvent(article.ArticleId, article.Title, article.Description));

            return "Article Updated";
        }

        public async Task<string> RemoveArticleAsync(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
                throw new Exception("article with this id does not exist");

            _context.Articles.Remove(article);

            if (await _context.SaveChangesAsync() < 1)
                throw new Exception("problem has occurred");

            await _endpoint.Publish(new RemoveArticleEvent(article.ArticleId));

            return "Article Removed";
        }
    }
}