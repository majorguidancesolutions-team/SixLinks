using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataModels
{
    public class Movie_Actor
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int MovieId { get; set; }
        [Required]
        public int ActorId { get; set; }
        public virtual Movie? Movie { get; set; }
        public virtual Actor? Actor { get; set; }

    }
}
