using System;

namespace Business.Entities
{
    public class Url
    {
        public Guid Id { get; set; }
        public string ShortUrl { get; set; }
        public string OriginUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public long Traffic { get; set; }
    }
}
