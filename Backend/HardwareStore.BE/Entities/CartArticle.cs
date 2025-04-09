using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HardwareStore.BE.Entities
{
    [Table("CartArticles", Schema = "dbo")]
    public class CartArticle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public Article? Article { get; set; }

        [Required]
        [ForeignKey(nameof(Article))]
        public int ArticleId { get; set; }
        public int Amount { get; set; }
        public DateTime AddedDate { get; set; }

    }
}
