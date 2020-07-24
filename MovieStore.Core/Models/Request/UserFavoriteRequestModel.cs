using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieStore.Core.Models.Request
{
    public class UserFavoriteRequestModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int MovieId { get; set; }
    }
}
