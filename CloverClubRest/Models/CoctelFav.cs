using System;
using System.Collections.Generic;

namespace CloverClubRest.Models
{
    public partial class CoctelFav
    {
        public int Userid { get; set; }
        public int Coctelid { get; set; }

        public User User { get; set; }
    }
}
