using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rapport.Domaine.Models
{
    public partial class Horaire
    {
        [Key]
        public Guid Id { get; set; }
        public Guid MyUserId { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime DateDuJourHor { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime HeureArriveHor { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime HeureDepartHor { get; set; }
    }
}
