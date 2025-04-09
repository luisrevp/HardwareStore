using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareStore.BE.Entities
{
    [Table("Articles", Schema = "dbo")]
    public class Article
    {
        public Article() { }
        public Article(string articleName, string articleDescription, string articleType, double articlePrice) 
        {
            this.ArticleName = articleName;
            this.ArticleDescription = articleDescription;
            this.ArticleType = articleType;
            this.ArticlePrice = articlePrice;
        }

        public Article(int id, string articleName, string articleDescription, string articleType, double articlePrice) 
            : this(articleName, articleDescription, articleType, articlePrice)
        {
            this.Id = id;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string ArticleName { get; set; }

        [Required]
        [MaxLength(100)]
        public string ArticleDescription { get; set; }

        [Required]
        [MaxLength(50)]
        public string ArticleType { get; set; }

        [Required]
        public double ArticlePrice { get; set; }
    }
}
