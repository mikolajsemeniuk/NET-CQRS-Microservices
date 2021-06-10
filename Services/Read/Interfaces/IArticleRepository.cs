using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Read.Payloads;

namespace Read.Interfaces
{
    public interface IArticleRepository
    {
        Task<IEnumerable<ArticlePayload>> GetArticlesAsync();
        Task<ArticlePayload> GetArticleAsync(Guid id);
    }
}