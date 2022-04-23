using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataModels
{
    public class Movie
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(1000)]
        public string Title { get; set; }
        [Required]
        public int Year { get; set; }
        [NotMapped]
        public string Crew { get; set; } = string.Empty;
        public virtual List<Movie_Actor> MovieActors { get; set; } = new List<Movie_Actor>();

        public override string ToString()
        {
            return $"{Title}";
        }
    }
}
