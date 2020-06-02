using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobilePaywall.FlowInspector.DataSet
{
  public class MerchantTemplateModel
  {
    private Data.Merchant _merchant = null;
    private List<ServiceTemplateModel> _services = null;

    public int ID { get { return this._merchant.ID; } }
    public string Name { get { return this._merchant.Name; } }
    public List<ServiceTemplateModel> Services { get { return this._services; } set { this._services = value; } }

    public ServiceTemplateModel this[int serviceID] { get { return (from s in this._services where s.ID == serviceID select s).FirstOrDefault(); } }

    public MerchantTemplateModel(Data.Merchant m)
    {
      this._merchant = m;
      this._services = new List<ServiceTemplateModel>();
    }

  }
}