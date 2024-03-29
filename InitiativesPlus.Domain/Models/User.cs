﻿namespace InitiativesPlus.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public UserRole UserRole { get; set; }
        public int StatusId { get; set; }
        public UserStatus UserStatus { get; set; }
    }
}
