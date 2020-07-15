using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieStore.Core.Entities
{
    public class Purchase
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public Guid PurchaseNumber { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public DateTime PurchaseDateTime { get; set; }

        [Required]
        public int MovieId { get; set; }

        [Required]
        public Movie Movie { get; set; }
        public User Customer { get; set; }
    }
}
