using HardwareStore.BE.Entities;

namespace HardwareStore.BE.Services
{
    public interface IArticleService
    {
        Task<IEnumerable<Article>> GetArticles(bool isDescending);
        Task<Article> GetArticle(int id);
        Task<bool> IsItemAdded(Article article);
        Task PublishArticle(Article article);
        Task DeleteArticle(Article article);
    }
}
