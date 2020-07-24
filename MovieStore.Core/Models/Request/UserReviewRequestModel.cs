using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieStore.Core.Models.Request
{
    public class UserReviewRequestModel
    {
        [Required]
        public int MovieId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public decimal Rating { get; set; }


        public string ReviewText { get; set; }
    }
}
