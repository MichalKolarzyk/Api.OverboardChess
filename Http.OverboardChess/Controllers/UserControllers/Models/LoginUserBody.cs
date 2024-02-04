﻿using System.ComponentModel.DataAnnotations;

namespace Http.OverboardChess.Controllers.UserControllers.Models
{
    public class LoginUserBody
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
