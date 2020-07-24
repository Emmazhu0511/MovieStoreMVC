using MovieStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStore.Core.Models.Response
{
    public class UserPurchaseResponseModel
    {
        //public int Id { get; set; }

        //public int UserId { get; set; }

        //public Guid PurchaseNumber { get; set; }

        //public decimal TotalPrice { get; set; }

        //public DateTime PurchaseDateTime { get; set; }

        //public int MovieId { get; set; }

        public bool IsMoviePurchased { get; set; }

        public bool IsMovieReviewed { get; set; }

        public bool IsMovieFavorited { get; set; }

        public Movie movie { get; set; }

        public Review review { get; set; }
    }
}
