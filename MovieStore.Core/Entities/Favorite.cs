using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStore.Core.Entities
{
    public class Favorite
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public int userId { get; set; }

        public Movie Movie { get; set; }

        public User User { get; set; }
    }
}
