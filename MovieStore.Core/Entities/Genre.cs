using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MovieStore.Core.Entities
{
    //Genre class represents our domain model, we are gonna have all the properties of Genre table column

    [Table("Genre")]
    public class Genre
    {
        
        //By convention EF ID gonna conside any property in the entity class as primary key for ID property
        public int Id { get; set; }

        [MaxLength(64)]
        [Required]
        public string Name { get; set; }
    }
}
