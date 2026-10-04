using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    internal class MSG
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? User { get; set; }
        public string? Recipients { get; set; }
        public string? Text { get; set; }
        public byte[]? FileData { get; set; }
        public string? FileName { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
