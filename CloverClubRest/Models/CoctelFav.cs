using System;
using System.Collections.Generic;

namespace CloverClubRest.Models
{
    public partial class CoctelFav
    {
        public long Userid { get; set; }
        public long Coctelid { get; set; }

        public User User { get; set; }
    }
}
