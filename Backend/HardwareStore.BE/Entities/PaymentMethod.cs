using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace HardwareStore.BE.Entities
{
    [Table("PaymentMethods", Schema = "dbo")]
    public class PaymentMethod
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Required] public string PaymentType { get; set; }
        [Required] public string CardNumber { get; set; }
        [Required] public string CardName { get; set; }
        [Required] public DateTime ExpirationDate {  get; set; }
        [Required] public int SecurityCode { get; set; }
        [Required] public User CardHolder { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        
    }
}
