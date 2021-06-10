using System;
using System.Threading.Tasks;
using Events;
using MassTransit;
using Read.Data;
using Read.Models;

namespace Read.Subscribers
{
    public class ArticleAddedConsumer : IConsumer<AddArticleEvent>
    {
        private readonly DataContext _context;
        private readonly IPublishEndpoint _endpoint;

        public ArticleAddedConsumer(DataContext context, IPublishEndpoint endpoint)
        {
            _context = context;
            _endpoint = endpoint;
        }

        public async Task Consume(ConsumeContext<AddArticleEvent> context)
        {
            _context.Articles.Add(new Article
            {
                ArticleId = context.Message.id,
                Title = context.Message.title,
                Description = context.Message.description
            });

            if (await _context.SaveChangesAsync() < 1)
                throw new Exception("problem has occurred");
        }
    }
}