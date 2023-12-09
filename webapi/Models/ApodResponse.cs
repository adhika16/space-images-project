namespace webapi.Models
{
    public class ApodResponse
    {
        public Dictionary<string, string> Resource { get; set; } = new Dictionary<string, string>();
        public bool ConceptTags { get; set; }
        public string? Title { get; set; }
        public DateTime Date { get; set; }
        public string? Url { get; set; }
        public string? HdUrl { get; set; }
        public string? MediaType { get; set; }
        public string? Explanation { get; set; }
        public List<string> Concepts { get; set; } = new List<string>();
        public string? ThumbnailUrl { get; set; }
        public string? Copyright { get; set; }
        public string? ServiceVersion { get; set; }
    }

}
