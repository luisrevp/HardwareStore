using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareStore.BE.Entities
{
    [Table("OrderArticles", Schema = "dbo")]
    public class OrderArticle
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Article))]
        public int ArticleId { get; set; }

        [Required]
        public int Amount { get; set; }
        [Required]
        public Article? Article { get; set; }
    }
}