using HardwareStore.BE.Entities;
using HardwareStore.BE.DbContext;
using Microsoft.EntityFrameworkCore;

namespace HardwareStore.BE.Services
{
    public class ArticleService : IArticleService
    {
        private readonly HardwareStoreDbContext _hardwareRepository;
        public ArticleService(HardwareStoreDbContext hardwareRepository) 
        {
            this._hardwareRepository = hardwareRepository ?? throw new ArgumentNullException(nameof(hardwareRepository));
        }

        public async Task<IEnumerable<Article>> GetArticles(bool isDescending)
        {
            return isDescending 
                ? await _hardwareRepository.Articles.OrderByDescending(art => art.Id).ToListAsync()
                : await _hardwareRepository.Articles.ToListAsync();
        }

        public async Task<Article> GetArticle(int id) {
            return await _hardwareRepository.Articles
                .FirstOrDefaultAsync(a => a.Id == id) ?? throw new Exception("Article not found...");
        }

        public async Task<bool> IsItemAdded(Article article) 
        {
            return await _hardwareRepository.Articles.AnyAsync(art => 
                art.ArticleName == article.ArticleName && 
                art.ArticleDescription == article.ArticleDescription);
        }

        public async Task PublishArticle(Article article)
        {
            _hardwareRepository.Articles.Add(article);
            await this.SaveChanges();
        }

        public async Task DeleteArticle(Article article)
        {
            if (await IsItemAdded(article)) 
            {
                _hardwareRepository.Articles.Remove(article);
                await this.SaveChanges();
            }
        }

        private async Task SaveChanges()
        {
            await _hardwareRepository.SaveChangesAsync();
        }
    }
}
