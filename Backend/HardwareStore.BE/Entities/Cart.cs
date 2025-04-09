using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace HardwareStore.BE.Entities
{
    [Table("Carts", Schema = "dbo")]
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public ICollection<CartArticle> Articles { get; set; } = new List<CartArticle>();
        [ForeignKey(nameof(User))][Required] public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
