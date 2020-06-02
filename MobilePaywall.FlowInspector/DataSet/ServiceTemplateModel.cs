using MobilePaywall.Data;
using MobilePaywall.Direct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobilePaywall.FlowInspector.DataSet
{
  public class ServiceTemplateModel
  {
    private Service _service = null;
    private string _providerName = string.Empty;
    private List<TemplateServiceFlow> _templateServiceFlows = null;
    private List<TemplateServiceFlowEntry> _templateFlowEnties = null;

    public int ID { get { return this._service.ID; } }
    public Service ServiceData { get { return this._service; } }
    public string Name { get { return this._service.Name; } }
    public string ProviderName { get { return this._providerName; } }
    
    public List<TemplateServiceFlow> Flows { get { return this._templateServiceFlows; } }
    public List<TemplateServiceFlowEntry> FlowEnttries { get { return this._templateFlowEnties; } }

    public ServiceTemplateModel(Service s)
    {
      this._service = s;
      this._providerName = MobilePaywallDirect.Instance.LoadString(string.Format(@"
        SELECT pp.Name FROM MobilePaywall.core.ServiceOffer AS so
        LEFT OUTER JOIN MobilePaywall.core.PaymentConfiguration AS pc ON so.PaymentConfigurationID=pc.PaymentConfigurationID
        LEFT OUTER JOIN MObilePaywall.core.PaymentProvider AS pp ON pc.PaymentProviderID=pp.PaymentProviderID
        WHERE so.ServiceID={0};", s.ID));


      this._templateServiceFlows = TemplateServiceFlow.CreateManager().Load(this._service);
      if (this._templateServiceFlows == null)
        this._templateServiceFlows = new List<TemplateServiceFlow>();

      this._templateFlowEnties = TemplateServiceFlowEntry.CreateManager().Load(this._service);
      if (this._templateFlowEnties == null)
        this._templateFlowEnties = new List<TemplateServiceFlowEntry>();
    }

    public List<TemplateServiceFlowEntry> GetEntriesByFlowID(int id)
    {
      return (from e in this._templateFlowEnties where e.TemplateServiceFlow.ID == id select e).ToList();
    }

  }
}