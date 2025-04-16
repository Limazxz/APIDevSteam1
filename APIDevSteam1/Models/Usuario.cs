﻿using Microsoft.AspNetCore.Identity;

namespace APIDevSteam1.Models
{
    public class Usuario : IdentityUser
    {
        public Usuario() : base()
        { }

        public string? NomeCompleto { get; set; }
        public DateOnly DataNascimento { get; set; }
    }
}