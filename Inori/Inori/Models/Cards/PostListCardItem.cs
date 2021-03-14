using Inori.Models.randomc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inori.Models.Cards
{
    public class PostListCardItem
    {
        public bool Viewed { get; set; }
        public RandomCPost Post { get; set; }
        public string Summary { get; set; }
        public string TimeAgo { get; set; }
        public Uri CoverImage { get; set; }

        public PostListCardItem()
        {
            Viewed = false;
            Post = null;
            Summary = "";
            TimeAgo = "";
        }
    }
}
