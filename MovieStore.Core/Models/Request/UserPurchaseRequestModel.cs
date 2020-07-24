using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieStore.Core.Models.Request
{
    public class UserPurchaseRequestModel
    {

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

        public UserPurchaseRequestModel() {

            PurchaseDateTime = DateTime.UtcNow;

            PurchaseNumber = Guid.NewGuid();
        }
    }
}
