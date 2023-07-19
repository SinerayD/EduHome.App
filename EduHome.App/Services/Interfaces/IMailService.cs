﻿using System;
namespace EduHome.App.Services.Interfaces
{
    public interface IMailService
    {
        public Task Send(string from, string to, string link, string text, string subject);
    }
}