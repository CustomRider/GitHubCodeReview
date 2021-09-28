using Newtonsoft.Json;
using System;

namespace GitHubTask.Models
{
    public class Reaction
    {
        public int Id { get; set; }
        [JsonProperty("node_id")]
        public string NodeId { get; set; }
        public User User { get; set; }
        public string Content { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
