using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProductsyCategories.Models
{
    public class Product
        {
            [Key]
            public int ProductId { get; set; }

            [Required]
            [MinLength(2)]
            public string Name {get; set;}

            [Required]
            [MinLength(2)]
            public string Description {get; set;}

            public double price {get; set;}
            public DateTime CreatedAt {get;set;} = DateTime.Now;
            public DateTime UpdatedAt {get;set;} = DateTime.Now; 
            public List<Association> ProductsCategories {get; set;}

        }
}