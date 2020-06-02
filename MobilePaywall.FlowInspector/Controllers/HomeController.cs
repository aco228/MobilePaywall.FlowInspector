using MobilePaywall.Direct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MobilePaywall.Data;
using MobilePaywall.FlowInspector.Models;
using MobilePaywall.FlowInspector.DataSet;

namespace MobilePaywall.FlowInspector.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {

      //HomeModel model = new HomeModel();
      

      //MobilePaywallDirect db = MobilePaywallDirect.Instance;

      //int? serviceID = db.LoadInt(string.Format("SELECT ServiceID FROM MobilePaywall.core.Service WHERE Name='{0}'"));


      //DirectContainer container = db.LoadContainer(string.Format(@"SELECT s.Name AS 'SName', m.Name AS 'MName', s.ServiceGuid FROM MobilePaywall.core.Service AS s 
      //                                                               LEFT OUTER JOIN MobilePaywall.core.Merchant AS m ON s.MerchantID=m.MerchantID"));
      //if (container.HasValue)
      //  return null;

      //string serviceName1 = container.GetString("SName", 1);

      //foreach(DirectContainerRow row in container.Rows)
      //{
      //  string serviceName = container.GetString("MName");
      //  Guid? serviceGuid = container.GetGuid("ServiceGuid");
      //}

      //db.Execute("UPDATE blla");

      return View(new HomeModel(FlowInspectorApplication.CountryTemplateModel));
    }
    
    public ActionResult ServiceFlowsView()
    {
      string sid = Request["sid"] != null ? Request["sid"].ToString() : string.Empty;

      int serviceID;
      if (!Int32.TryParse(sid, out serviceID))
        return this.Content("Service id is not set");

      ServiceTemplateModel serviceTemplateModel = FlowInspectorApplication.GetServiceModelByString(sid);
      if (serviceTemplateModel == null)
        return this.Content("ServiceTemplateModel is empty") ;

      ServiceFlowsViewModel model = new ServiceFlowsViewModel();
      model.Flows = serviceTemplateModel.Flows;
      
      return PartialView("_ServiceFlow", model);
      //return this.Json(new { status = true, url = Url.Action("_ServiceFlow", vm) }, JsonRequestBehavior.AllowGet);
    }

    public ActionResult AddFlow()
    {
      string _title = Request["title"] != null ? Request["title"].ToString() : string.Empty;
      string _serviceID = Request["serviceID"] != null ? Request["serviceID"].ToString() : string.Empty;
      string _flowID = Request["flowID"] != null ? Request["flowID"].ToString() : string.Empty;
      
      ServiceTemplateModel serviceModel = FlowInspectorApplication.GetServiceModelByString(_serviceID);
      if(serviceModel == null)
        return this.Json(new { status = false, message = "Internal error. COuld not load serviceModel by id:" + _serviceID });
      
      if (string.IsNullOrEmpty(_title))
        return this.Json(new { strus = false, message = "Title is not set!" });

      if (string.IsNullOrEmpty(_serviceID))
        return this.Json(new { status = false, message = "ServiceID is not set" });
      
      int flowID;
      if(!Int32.TryParse(_flowID, out flowID))
        return this.Json(new { status = false, message = "FlowID is not set" });

      if(flowID == -1)
      {
        TemplateServiceFlow ts = new TemplateServiceFlow(flowID, serviceModel.ServiceData, _title, DateTime.Now, DateTime.Now);
        ts.Insert();

        serviceModel.Flows.Add(ts);
        return this.Json(new { status = true, message = "Inserted", id = ts.ID });
      }
      else
      {
        TemplateServiceFlow ts = TemplateServiceFlow.CreateManager().Load(flowID);
        ts.Title = _title;
        ts.Update();
        return this.Json(new { status = true, message = "Updated!", id = ts.ID });
      }

     
    }


  }
}