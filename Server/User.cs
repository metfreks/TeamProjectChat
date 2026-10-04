using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    internal class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsApproved { get; set; }
        public bool IsBanned { get; set; }
        public DateTime? Ban { get; set; }

        public override string ToString()
        {
            string result = !IsApproved ? $"Awaiting approval" :(IsBanned && Ban > DateTime.UtcNow) ? $"Banned until {Ban:HH:mm}":"Active";
            return $"{Name}: {result}";
        }
    }
}
