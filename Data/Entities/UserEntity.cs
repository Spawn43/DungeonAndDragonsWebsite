﻿using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class UserEntity
    {
        [Key]
        public  string Id { get; set; }
        public  string Username { get; set; }
        public  string Email { get; set; }
        public  string? PasswordSalt { get; set; }
        public  string? PasswordHash { get; set; }
        public  string FirstName { get; set; }
        public  string Surname { get; set; }
        public  DateTime DateOfBirth { get; set; }


    }
}
