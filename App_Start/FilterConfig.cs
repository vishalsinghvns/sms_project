﻿using System.Web;
using System.Web.Mvc;

namespace demo.smart_school
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
