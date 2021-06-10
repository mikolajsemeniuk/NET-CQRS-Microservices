using System;

namespace Read.Payloads
{
    public class ArticlePayload
    {
        public Guid ArticleId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}