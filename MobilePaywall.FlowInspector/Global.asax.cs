using MobilePaywall.Data;
using MobilePaywall.FlowInspector.DataSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MobilePaywall.FlowInspector
{
  public class FlowInspectorApplication : System.Web.HttpApplication
  {

    public static List<CountryTemplateModel> CountryTemplateModel = null;
    

    protected void Application_Start()
    {
      
      MobilePaywall.Data.Sql.Database db = null;
      Senti.Data.DataLayerRuntime r = new Senti.Data.DataLayerRuntime();

      AreaRegistration.RegisterAllAreas();
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);

      this.PrepareTemplateList();
    }


    private void PrepareTemplateList()
    {
      FlowInspectorApplication.CountryTemplateModel = new List<DataSet.CountryTemplateModel>();

      List<Service> allServices = Service.CreateManager().Load();
      List<Data.Merchant> addedMerchant = new List<Data.Merchant>();
      
      foreach(Service s in allServices)
      {
        CountryTemplateModel countryTemplateModel = this.GetByCountry(s.FallbackCountry);

        if (countryTemplateModel == null)
        {
          countryTemplateModel = new DataSet.CountryTemplateModel(s.FallbackCountry);
          FlowInspectorApplication.CountryTemplateModel.Add(countryTemplateModel);
        }

        MerchantTemplateModel merchantTemplateModel = countryTemplateModel[s.Merchant.ID];
        if(merchantTemplateModel == null)
        {
          merchantTemplateModel = new MerchantTemplateModel(s.Merchant);
          countryTemplateModel.Merchants.Add(merchantTemplateModel);
        }

        ServiceTemplateModel serviceTemplateModel = merchantTemplateModel[s.ID];
        if(serviceTemplateModel == null)
        {
          serviceTemplateModel = new ServiceTemplateModel(s);
          merchantTemplateModel.Services.Add(serviceTemplateModel);
        }

      }
    }

    private CountryTemplateModel GetByCountry(Country c)
    {
      foreach (CountryTemplateModel t in FlowInspectorApplication.CountryTemplateModel)
        if (t.ID == c.ID)
          return t;
      return null;
    }

    public static ServiceTemplateModel GetServiceModelByString(string sid)
    {
      int serviceID;
      if (!Int32.TryParse(sid, out serviceID))
        return null;

      return FlowInspectorApplication.GetServiceModel(Service.CreateManager().Load(serviceID));
    }

    public static ServiceTemplateModel GetServiceModel(Service service)
    {
      CountryTemplateModel ctm = (from c in FlowInspectorApplication.CountryTemplateModel where c.ID == service.FallbackCountry.ID select c).FirstOrDefault();
      if (ctm == null)
        return null;

      MerchantTemplateModel mtm = (from m in ctm.Merchants where m.ID == service.Merchant.ID select m).FirstOrDefault();
      if (mtm == null)
        return null;

      return (from s in mtm.Services where s.ID == service.ID select s).FirstOrDefault();
    }

  }
}
