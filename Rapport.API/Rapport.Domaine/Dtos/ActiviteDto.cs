using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Rapport.Domaine.Dtos
{
    public class ActiviteDto
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
