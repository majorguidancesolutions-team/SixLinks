using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataModels
{
    public class Actor
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        public int? BaconRating { get; set; }

        public virtual List<Movie_Actor> ActorMovies { get; set; } = new List<Movie_Actor>();

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
