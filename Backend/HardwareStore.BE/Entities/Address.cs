using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareStore.BE.Entities
{
    [Table("Addresses", Schema = "dbo")]
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required] public string Street { get; set; }

        [Required] public int StreetNumber { get; set; }
        [Required] public string City { get; set; }
        [Required] public string State { get; set; }
        [Required] public string PostalCode { get; set; }
        [Required] public string Country { get; set; }
        [Required] public User? User { get; set; }
    }
}
