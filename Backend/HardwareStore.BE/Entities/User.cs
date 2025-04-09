using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace HardwareStore.BE.Entities
{   
    public class User : IdentityUser
    {
        [Required] public string FirstName { get; set; } = string.Empty;
        [Required] public string LastName { get; set; } = string.Empty;
        [Required] public DateTime DoB {  get; set; }
        [ForeignKey(nameof(Cart))]
        public int? CartId { get; set; } = null;
        public Cart? Cart { get; set; } = null;
        public ICollection<Order>? Orders { get; set; } // Navigation property
        public ICollection<Address>? ListOfAddress { get; set; } // Navigation property
        public ICollection<PaymentMethod>? PaymentMethods { get; set; } // Navigation property
    }
}
