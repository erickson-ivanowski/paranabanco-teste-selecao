using ParanaBanco.Service.Customers.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParanaBanco.Service.Customers.Infrastructure.Data.Models
{
    [Table("Customers")]
    public class Customer : IDataModel
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FullName { get; set; }
    }
}
