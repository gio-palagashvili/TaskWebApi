﻿using System.Collections.Generic;

namespace TaskWebApi
{
    public class RequestLog
    {
        public string Id { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        public string StatusCode { get; set; }
        public string Host { get; set; }
        public string Headers { get; set; }
    }
}