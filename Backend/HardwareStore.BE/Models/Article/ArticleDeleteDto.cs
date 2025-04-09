using System.ComponentModel.DataAnnotations;

namespace HardwareStore.BE.Models.Article
{
    public class ArticleDeleteDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ArticleName { get; set; }
        public string ArticleDescription { get; set; }
        public string ArticleType { get; set; }
        public double ArticlePrice { get; set; }
    }
}
