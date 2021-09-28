using Newtonsoft.Json;
using System;

namespace GitHubTask.Models
{
    public class Comment
    {
        public string Url { get; set; }
        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }
        public int Id { get; set; }
        [JsonProperty("node_id")]
        public string NodeId { get; set; }
        public User User { get; set; }
        public int? Position { get; set; }
        public int? Line { get; set; }
        public string Path { get; set; }
        [JsonProperty("commit_id")]
        public string CommitId { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("author_association")]
        public string AuthorAssociation { get; set; }
        public string Body { get; set; }
    }
}
