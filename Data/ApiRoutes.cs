using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CostApp.Data
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version1 = "v1";
        public const string Base = Root + "/" + Version1;
        public const string ControllerBase = Base + "/[controller]";

        public static class Categories
        {
            public const string GetAll = Base + "/categories";
        }

        public static class Expense
        {
            public const string Create = Base + "/expense";
            public const string GetAllWithDatesAndCategories = Base + "/expense";
        }

        public static class Identity
        {
            public const string Login = Base + "/identity/login";
            public const string Register = Base + "/identity/register";
            public const string Refresh = Base + "/identity/refresh";
            public const string ConfirmEmail = Base + "/identity/emailconfirm";
            public const string Update = Base + "/identity/update";
        }
    }
}
