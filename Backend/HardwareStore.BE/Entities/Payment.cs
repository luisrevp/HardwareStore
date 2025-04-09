using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareStore.BE.Entities
{
    [Table("Payments", Schema = "dbo")]
    public class Payment
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User? User { get; set; }
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
    }
}
