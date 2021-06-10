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

        public ArticleAddedConsumer(DataContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<AddArticleEvent> context)
        {
            Console.WriteLine("hej cos sie tu trigeruje");
            _context.Articles.Add(new Article
            {
                ReadId = context.Message.id,
                Title = context.Message.title,
                Description = context.Message.description
            });

            if (await _context.SaveChangesAsync() < 1)
                throw new Exception("problem has occurred");
        }
    }
}