using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public List<Guid> Groups { get; set; }
    }
}