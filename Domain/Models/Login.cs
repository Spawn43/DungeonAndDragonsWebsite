﻿namespace Domain.Models
{
    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public int TTL { get; set; }
    }
}
