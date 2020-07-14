using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rapport.Domaine.Models
{
    public partial class MyUser
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [StringLength(160)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(160)]
        public string LastName { get; set; }
       
        [Required]
        [StringLength(20)]
        public string Phone { get; set; }
       
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        public string UserLogin { get; set; }
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        [StringLength(255)]
        public string Token { get; set; }
       
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedAt { get; set; }
    }
}
