using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataModels
{
    public class Item
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
        //even though the category is required, allow this to 
        //be null so that it can be ignored
        public virtual Category? Category { get; set; }
    }
}
