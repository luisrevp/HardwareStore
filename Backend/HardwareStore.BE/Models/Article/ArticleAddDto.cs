using System.ComponentModel.DataAnnotations;

namespace HardwareStore.BE.Models.Article
{
    public class ArticleAddDto
    {
        [Required(ErrorMessage = "You must provide a name")]
        [MinLength(3)]
        [MaxLength(150)]
        public string ArticleName { get; set; }

        [Required(ErrorMessage = "You must provide a description")]
        [MinLength(10)]
        [MaxLength(250)]
        public string ArticleDescription { get; set; }

        [Required(ErrorMessage = "You must provide a type")]
        [MinLength(3)]
        [MaxLength(50)]
        public string ArticleType { get; set; }

        [Required]
        public double ArticlePrice { get; set; }
    }
}
