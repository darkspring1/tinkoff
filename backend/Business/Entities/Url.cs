﻿using System;

namespace Business.Entities
{
    public class Url
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public string Origin { get; set; }
        public DateTime CreatedAt { get; set; }
        public long Traffic { get; set; }
    }
}
