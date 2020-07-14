using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rapport.Domaine.Models
{
    public partial class Activite
    {
        [Key]
        public Guid Id { get; set; }
        public Guid MyUserId { get; set; }
        [Required]
        [StringLength(255)]
        public string ContenuAct { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime DateAct { get; set; }
    }
}
