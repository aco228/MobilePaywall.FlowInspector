﻿using System.Web;
using System.Web.Mvc;

namespace MobilePaywall.FlowInspector
{
  public class FilterConfig
  {
    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    {
      filters.Add(new HandleErrorAttribute());
    }
  }
}
