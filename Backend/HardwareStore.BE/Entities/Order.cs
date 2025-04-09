using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareStore.BE.Entities
{
    [Table("Orders", Schema = "dbo")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required] public ICollection<OrderArticle>? OrderArticles { get; set; } = null;

        [Required] public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get;set; }
    }
}
