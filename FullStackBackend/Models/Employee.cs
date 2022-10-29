using System.ComponentModel.DataAnnotations;

namespace FullStackBackend.Models
{
    public class Employee{
        [Key]
        public Guid Id {get;set;}
        [Required]
        public string Name {get;set;}
        [Required]
        public string Email {get;set;}
        [Required]
        [MaxLength(9)]
        public string Phone {get;set;}
        [Required]
        public string Salary {get;set;}
    }
}