using System;
using System.Collections.Generic;

namespace CloverClubRest.Models
{
    public partial class IngredienteFav
    {
        public long Userid { get; set; }
        public long Ingredienteid { get; set; }

        public User User { get; set; }
    }
}
