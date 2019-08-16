using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entities
{
    public class User
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Guid Salt { get; set; }
    }
}
