using MobilePaywall.FlowInspector.DataSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobilePaywall.FlowInspector.Models
{
  public class HomeModel
  {

    private List<CountryTemplateModel> _list = null;
    public List<CountryTemplateModel> List { get { return this._list; } }

    public HomeModel(List<CountryTemplateModel>  list)
    {
      this._list = list;
    }

  }
}