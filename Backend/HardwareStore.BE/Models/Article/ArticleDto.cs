namespace HardwareStore.BE.Models.Article
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string ArticleName { get; set; } = string.Empty;
        public string ArticleDescription { get; set; } = string.Empty;
        public string ArticleType { get; set; } = string.Empty;
        public double ArticlePrice { get; set; }

    }
}
