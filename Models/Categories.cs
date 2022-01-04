using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProductsyCategories.Models
{
    public class Category
        {
            [Key]
            public int CategoryId { get; set; }

            [Required]
            [MinLength(2)]
            public string Name {get; set;}
            public DateTime CreatedAt {get;set;} = DateTime.Now;
            public DateTime UpdatedAt {get;set;} = DateTime.Now; 
            public List<Association> CategoriesProducts {get; set;}

        }
}