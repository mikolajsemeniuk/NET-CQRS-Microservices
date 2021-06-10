using System;
using System.Threading.Tasks;
using Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Read.Data;

namespace Read.Subscribers
{
    public class ArticleUpdatedConsumer : IConsumer<UpdateArticleEvent>
    {
        private readonly DataContext _context;

        public ArticleUpdatedConsumer(DataContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<UpdateArticleEvent> context)
        {
            var article = await _context.Articles.SingleAsync(
                article => article.ReadId == context.Message.id);

            article.Title = context.Message.title;
            article.Description = context.Message.description;

            if (await _context.SaveChangesAsync() < 1)
                throw new Exception("problem has occurred");
        }
    }
}