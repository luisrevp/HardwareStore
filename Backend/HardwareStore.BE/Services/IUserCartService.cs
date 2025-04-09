using HardwareStore.BE.Entities;

namespace HardwareStore.BE.Services
{
    public interface IUserCartService
    {
        Task<Cart> GetCartByUser(string userId);
        Task AddArticleToCart(int articleId, string userId, int amount);
        Task<bool> IsArticleInCart(Cart cart, Article article);
        Task DeletArticleFromCart(int articleId, string userId);
    }
}
