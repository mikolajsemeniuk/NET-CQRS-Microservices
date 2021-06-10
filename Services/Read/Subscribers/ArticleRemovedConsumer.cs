using System;
using System.Threading.Tasks;
using Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Read.Data;

namespace Read.Subscribers
{
    public class ArticleRemovedConsumer : IConsumer<RemoveArticleEvent>
    {
        private readonly DataContext _context;

        public ArticleRemovedConsumer(DataContext context)
        {
            _context = context;
        }
        
        public async Task Consume(ConsumeContext<RemoveArticleEvent> context)
        {
            var article = await _context.Articles.SingleAsync(
                article => article.ReadId == context.Message.id);

            _context.Articles.Remove(article);

            if (await _context.SaveChangesAsync() < 1)
                throw new Exception("problem has occurred");
        }
    }
}