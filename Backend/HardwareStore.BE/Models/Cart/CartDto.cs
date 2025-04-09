using HardwareStore.BE.Models.Article;

namespace HardwareStore.BE.Models.Cart
{
    public class CartDto
    {
        public int Id { get; set; }
        public List<ArticleDto> Articles { get; set; } = new List<ArticleDto>();
        public string? UserId { get; set; }
    }
}
