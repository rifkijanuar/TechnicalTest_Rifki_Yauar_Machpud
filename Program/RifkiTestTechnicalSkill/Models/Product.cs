﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RifkiTestTechnicalSkill.Models
{
    [Table("Prodcuts")]
    public class Product
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string? ProductName { get; set; }

        [Required]
        [MaxLength(40)]
        public string? AuthorName { get; set; }
        [Required]
        public double Price { get; set; }
        public string? Image { get; set; }
        [Required]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
        public List<CartDetail> CartDetail { get; set; }
        public Stock Stocks { get; set; }

        [NotMapped]
        public string GenreName { get; set; }
        [NotMapped]
        public int Quantity { get; set; }
    }
}
