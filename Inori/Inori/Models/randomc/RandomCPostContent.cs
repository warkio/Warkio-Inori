using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace Inori.Models.randomc
{
    public class RandomCPostContent
    {
        [JsonProperty("rendered")]
        public string Rendered;
        [JsonProperty("protected")]
        public Boolean Protected;

        /// <summary>
        /// Get all images inside the table of the post
        /// </summary>
        /// <returns>Array with the links to all images</returns>
        public List<Uri> GetImages()
        {
            
            List<Uri> imagesFound = new List<Uri>();
            if (this.Rendered == null)
            {
                return imagesFound;
            }
            var doc = new HtmlDocument();
            doc.LoadHtml(this.Rendered);
            HtmlNodeCollection imageNodes = doc.DocumentNode.SelectNodes("//table//img");

            // Sometimes, there are no images inside the post. In that case, return a empty list
            if(imageNodes != null)
            {
                foreach (HtmlNode image in imageNodes)
                {
                    string url = image.GetAttributeValue("src", null);
                    if (url != null)
                    {
                        imagesFound.Add(new Uri($"https:{url.Trim()}"));
                    }
                }
            }

            
            return imagesFound;
        }

        /// <summary>
        /// Get the text content of the post. All text after the table with the images
        /// </summary>
        /// <returns>Post content as string</returns>
        public string GetContent()
        {
            if (this.Rendered == null)
            {
                return "";
            }

            var doc = new HtmlDocument();
            doc.LoadHtml(this.Rendered);
            HtmlNodeCollection postContent = doc.DocumentNode.SelectNodes("//table"); ;
            if(postContent != null)
            {
                foreach (HtmlNode table in postContent)
                {
                    table.Remove();
                }
            }

            // Return the content adding a extra newline between paragraphs
            return doc.DocumentNode.InnerHtml.Trim().Replace("</p>", "</p></br><p>&nbsp;&nbsp;</p>");
        }
    }
}
