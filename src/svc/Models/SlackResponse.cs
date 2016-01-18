using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BryanPorter.SlackCmd.Models
{
    public enum ResponseType
    {
        InChannel,
        Ephemeral
    }

    public class SlackResponse
    {
        [JsonIgnore]
        public ResponseType ResponseType { get; set; }

        [JsonProperty("response_type")]
        public string JsonResponseType
        {
            get { return ResponseType == ResponseType.Ephemeral ? "ephemeral" : "in_channel"; }
            set
            {
                if (value.Equals("inchannel", StringComparison.OrdinalIgnoreCase) ||
                    value.Equals("in_channel", StringComparison.OrdinalIgnoreCase))
                {
                    ResponseType = ResponseType.InChannel;
                }
                else if (value.Equals("ephemeral", StringComparison.OrdinalIgnoreCase))
                {
                    ResponseType = ResponseType.Ephemeral;
                }

                throw new ArgumentException("The specified value does not match a member of the ResponseType enumeration.");
            }
        }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("attachments")]
        public IEnumerable<SlackResponseAttachment> Attachments { get; set; }
    }

    public class SlackResponseAttachment
    {
        [JsonProperty("fallback")]
        public string FallbackText { get; set; }

        [JsonProperty("color")]
        public string ColorCode { get; set; }

        [JsonProperty("pretext")]
        public string PreText { get; set; }

        [JsonProperty("author_name")]
        public string AuthorName { get; set; }

        [JsonProperty("author_link")]
        public string AuthorLink { get; set; }

        [JsonProperty("author_icon")]
        public string AuthorIcon { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("title_link")]
        public string TitleLink { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("fields")]
        public IEnumerable<SlackField> Fields { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("thumb_url")]
        public string ThumbUrl { get; set; }
    }

    public class SlackField
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("short")]
        public bool IsShort { get; set; }
    }
}
