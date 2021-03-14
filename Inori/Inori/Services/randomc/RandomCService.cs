using Inori.Models.randomc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Linq;
using System.Threading.Tasks;

namespace Inori.Services.randomc
{
    public class RandomCService
    {
        static string API_BASE = "https://randomc.net/wp-json/";
        private HttpClient Client;

        public RandomCService()
        {
            this.Client = new HttpClient();
        }

        public async Task<RandomCApiResponse> GetPosts(int page = 1, int limit = 50)
        {
            if (page < 1)
            {
                throw new ArgumentException("Page must be greater or equal than 1");
            }
            HttpResponseMessage response = await this.Client.GetAsync(API_BASE + $"wp/v2/posts?per_page={limit}&page={page}");
            if((int)response.StatusCode > 200)
            {
                throw new Exception("Error loading the data");
            }

            // JSON response
            string jsonString = await response.Content.ReadAsStringAsync();
            jsonString = HttpUtility.HtmlDecode(jsonString).Replace("\\/", "/");
            // WP header information of pages
            string totalElementsHeader = response.Headers.GetValues("X-WP-Total").FirstOrDefault();
            string totalPagesHeader = response.Headers.GetValues("X-WP-TotalPages").FirstOrDefault();
            int totalElements = totalElementsHeader == null ? Convert.ToInt32(totalElementsHeader) : -1;
            int totalPages = totalPagesHeader == null ? Convert.ToInt32(totalPagesHeader) : -1;
            // Object creation
            return new RandomCApiResponse(jsonString, page, totalPages, totalElements);
        }
    }
}
