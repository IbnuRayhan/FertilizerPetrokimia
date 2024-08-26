using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FertilizerPetrokimia.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        [MaxLength(200)]
        public string name { get; set; }
        [Required]
        [MaxLength(200)]
        public string type { get; set; }
        [Required]
        [MaxLength(500)]
        public string description { get; set; }
        [Required]
        public string price { get; set; }
    }
}
