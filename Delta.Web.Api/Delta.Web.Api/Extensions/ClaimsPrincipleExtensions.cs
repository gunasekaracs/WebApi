﻿using System.Security.Claims;

namespace Delta.Web.Api
{
    public static class ClaimsPrincipleExtensions
    {
        public static string? GetUserName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }
        public static int GetUserId(this ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "-1");
        }
    }
}