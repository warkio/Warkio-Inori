using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inori.Models.randomc
{
    public class RandomCPost
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("date_gmt")]
        public string DateGmt { get; set; }
        [JsonProperty("guid")]
        public RandomCPostGuid Guid { get; set; }
        [JsonProperty("modified")]
        public string Modified { get; set; }
        [JsonProperty("modified_gmt")]
        public string ModifiedGmt { get; set; }
        [JsonProperty("slug")]
        public string Slug { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
        [JsonProperty("title")]
        public RandomCPostGuid Title { get; set; }
        [JsonProperty("content")]
        public RandomCPostContent Content { get; set; }
        [JsonProperty("excerpt")]
        public RandomCPostContent Excerpt { get; set; }
        [JsonProperty("author")]
        public int Author { get; set; }
        [JsonProperty("featured_media")]
        public int FeaturedMedia { get; set; }
        [JsonProperty("comment_status")]
        public string CommentStatus { get; set; }
        [JsonProperty("ping_status")]
        public string PingStatus { get; set; }
        [JsonProperty("sticky")]
        public Boolean Sticky { get; set; }
        [JsonProperty("template")]
        public string Template { get; set; }
        [JsonProperty("format")]
        public string Format { get; set; }
        [JsonProperty("meta")]
        public RandomCPostMeta Meta { get; set; }
        [JsonProperty("categories")]
        public int[] Categories { get; set; }
        [JsonProperty("tags")]
        public string[] tags { get; set; }
        [JsonProperty("jetpack_featured_media_url")]
        public string JetpackFeaturedMediaUrl { get; set; }
        [JsonProperty("_links")]
        public RandomCPostLinks Links { get; set; }

        public string TimeAgo()
        {
            var dateTime = DateTime.Parse(this.Date);
            string result = string.Empty;
            var timeSpan = DateTime.Now.Subtract(dateTime);

            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                result = string.Format("{0} seconds ago", timeSpan.Seconds);
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                result = timeSpan.Minutes > 1 ?
                    String.Format("{0} minutes ago", timeSpan.Minutes) :
                    "A minute ago";
            }
            else if (timeSpan <= TimeSpan.FromHours(24))
            {
                result = timeSpan.Hours > 1 ?
                    String.Format("{0} hours ago", timeSpan.Hours) :
                    "An hour ago";
            }
            else if (timeSpan <= TimeSpan.FromDays(30))
            {
                result = timeSpan.Days > 1 ?
                    String.Format("{0} days ago", timeSpan.Days) :
                    "Yesterday";
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                result = timeSpan.Days > 30 ?
                    String.Format("{0} months ago", timeSpan.Days / 30) :
                    "A month ago";
            }
            else
            {
                result = timeSpan.Days > 365 ?
                    String.Format("{0} years ago", timeSpan.Days / 365) :
                    "A year ago";
            }

            return result;
        }
    }
}