using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CloverClubRest.Models
{
    public partial class User
    {
        public User()
        {
            CoctelesFav = new HashSet<CoctelFav>();
            IngredientesFav = new HashSet<IngredienteFav>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Pass { get; set; }
        [Required]
        public string Email { get; set; }

        public ICollection<CoctelFav> CoctelesFav { get; set; }
        public ICollection<IngredienteFav> IngredientesFav { get; set; }

        [NotMapped]
        public IEnumerable<int> CoctelesFavList
        {
            get { return CoctelesFav.Select(c => c.Coctelid); }
        }

        [NotMapped]
        public IEnumerable<int> IngredientesFavList
        {
            get { return IngredientesFav.Select(i => i.Ingredienteid); }
        }
    }
}
