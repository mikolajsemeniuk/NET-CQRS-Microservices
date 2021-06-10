using System;
using System.Threading.Tasks;
using Write.Inputs;

namespace Write.Interfaces
{
    public interface IArticleRepository
    {
        Task<string> AddArticleAsync(ArticleInput input);
        Task<string> RemoveArticleAsync(Guid id);
        Task<string> UpdateArticleAsync(Guid id, ArticleInput input);
    }
}