﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trustify.Backend.AdminService.Keycloak.Models
{
    public class KeycloakOptions
    {
        public string Url { get; set; } = string.Empty;

        public string Realm { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string RedirectUrlAfterProfileUpdate { get; set; } = string.Empty;

        public string ClientIdForUrlRedirectionAfterProfileUpdate { get; set; } = string.Empty;

        public int ProfileUpdateLinkLifeSpanInSeconds { get; set; } = 86400;

        public bool SendEmailToNewUsers { get; set; } = false;
    }
}
