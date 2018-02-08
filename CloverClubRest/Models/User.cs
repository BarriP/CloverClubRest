using System;
using System.Collections.Generic;

namespace CloverClubRest.Models
{
    public partial class User
    {
        public User()
        {
            CoctelesFav = new HashSet<CoctelFav>();
            IngredientesFav = new HashSet<IngredienteFav>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Pass { get; set; }

        public ICollection<CoctelFav> CoctelesFav { get; set; }
        public ICollection<IngredienteFav> IngredientesFav { get; set; }
    }
}
