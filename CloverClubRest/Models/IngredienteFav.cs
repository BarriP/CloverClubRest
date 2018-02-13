using System;
using System.Collections.Generic;

namespace CloverClubRest.Models
{
    public partial class IngredienteFav
    {
        public int Userid { get; set; }
        public string Ingrediente { get; set; }

        public User User { get; set; }
    }
}
