using System;
using System.Collections.Generic;

namespace CloverClubRest.Models
{
    public partial class IngredienteFav
    {
        public int Userid { get; set; }
        public int Ingredienteid { get; set; }

        public User User { get; set; }
    }
}
