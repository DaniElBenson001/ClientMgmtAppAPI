﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMgmtAppAPI.Common.Constants
{
    public static class ApiRoutes
    {
        public const string Version = "v1.0";
        public static class Users
        {
            public const string Base = "/users";

            public const string Register = "/";
            public const string GetInfo = "/{id}";
        }

        public static class Auth
        {
            public const string Base = "/auth";

            public const string Login = "/login";
        }

        public static class Clients
        {
            public const string Base = "/clients";

            public const string Create = "/";
            public const string ReadAll = "/";
            public const string Delete = "/delete";
            public const string Update = "/update";
        }

        public static class Search
        {
            public const string Base = "/search";

            public const string SearchClients = "/clients";
        }
    }
}
