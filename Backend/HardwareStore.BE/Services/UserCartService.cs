using HardwareStore.BE.Entities;
using HardwareStore.BE.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Routing;
using HardwareStore.BE.Models;

namespace HardwareStore.BE.Services
{
    public class UserCartService : IUserCartService
    {
        private readonly HardwareStoreDbContext _repository;
        private readonly IArticleService _hardwareService;

        public UserCartService(HardwareStoreDbContext repository, IArticleService hardwareService)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _hardwareService = hardwareService ?? throw new ArgumentNullException(nameof(hardwareService));
        }

        public async Task<Cart> GetCartByUser(string userId)
        {
            if (String.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId));

            var userCart = await _repository.Carts.SingleOrDefaultAsync(c => c.UserId == userId)
                ?? throw new ArgumentException($"No carts for userID {userId}");

            return userCart;
        }

        public async Task AddArticleToCart(int articleId, string userId, int amount)
        {
            Cart userCart = await this.GetCartByUser(userId);
            Article article = await this.RetrieveArticle(articleId);

            if (await this.IsArticleInCart(userCart, article))
                return;

            CartArticle cartArticle = new CartArticle()
            {
                Article = article,
                Amount = amount,
                AddedDate = DateTime.UtcNow
            };

            if (article != null)
            {
                userCart.Articles.Add(cartArticle);
                await this.SaveChanges();
            }
        }


        public async Task<bool> IsArticleInCart(Cart cart, Article article)
        {
            bool checkCartArticle = cart.Articles.Any(cartArticle => cartArticle.Article == article);
            return await Task.FromResult(checkCartArticle);
        }

        public async Task DeletArticleFromCart(int articleId, string userId)
        {
            CartArticle cartArticle = await this.GetArticleFromCart(articleId, userId);
            if (cartArticle != null)
            {
                Cart userCart = await this.GetCartByUser(userId);
                userCart.Articles.Remove(cartArticle);
                await this.SaveChanges();
            }
        }

        private async Task<Article> RetrieveArticle(int articleId)
            => await this._hardwareService.GetArticle(articleId) ??
                throw new ArgumentException("No articles available...");

        private async Task<CartArticle> GetArticleFromCart(int articleId, string userId)
        {
            Cart cart = await this.GetCartByUser(userId);
            Article article = await this.RetrieveArticle(articleId);

            if (!await IsArticleInCart(cart, article))
                return null;

            return cart.Articles.SingleOrDefault(art => art.Article == article);
        }

        private async Task SaveChanges()
        {
            await _repository.SaveChangesAsync();
        }
    }
}
