using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inori.Models.randomc
{
    public class RandomCApiResponse
    {
        public RandomCPost[] Posts { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalElements { get; set; }

        public RandomCApiResponse(string jsonPosts, int currentPage, int totalPages, int totalElement)
        {
            this.Posts = JsonConvert.DeserializeObject<RandomCPost[]>(jsonPosts);
            this.TotalPages = totalPages;
            this.CurrentPage = currentPage;
            this.TotalElements = totalElement;
        }
    }
}
