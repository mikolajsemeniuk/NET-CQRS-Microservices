using System.Threading.Tasks;
using Write.Inputs;

namespace Write.Interfaces
{
    public interface IArticleRepository
    {
        Task<string> AddArticleAsync(ArticleInput input);
        Task<string> RemoveArticleAsync(int id);
        Task<string> UpdateArticleAsync(int id, ArticleInput input);
    }
}