using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        public int Year { get; set; }

        public virtual List<Movie_Actor> MovieActors { get; set; } = new List<Movie_Actor>();
    }
}
