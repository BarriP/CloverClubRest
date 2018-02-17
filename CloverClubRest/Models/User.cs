using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;

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
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [MinLength(3)]
        public string Pass { get; set; }
        [Required]
        [MinLength(3)]
        public string Email { get; set; }

        [JsonIgnore]
        public ICollection<CoctelFav> CoctelesFav { get; set; }
        [JsonIgnore]
        public ICollection<IngredienteFav> IngredientesFav { get; set; }

        
        [NotMapped]
        public IEnumerable<int> CoctelesFavList
        {
            get { return CoctelesFav.Select(c => c.Coctelid); }
        }

        
        [NotMapped]
        public IEnumerable<string> IngredientesFavList
        {
            get { return IngredientesFav.Select(i => i.Ingrediente); }
        }
    }
}
