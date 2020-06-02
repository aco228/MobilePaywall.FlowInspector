using MobilePaywall.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobilePaywall.FlowInspector.Models
{
  public class ServiceFlowsViewModel
  {
    private List<TemplateServiceFlow> _flows = null;
    private List<TemplateServiceFlowEntry> _entries = null;
    
    public List<TemplateServiceFlow> Flows { get { return this._flows; } set { this._flows = value; } }
    public List<TemplateServiceFlowEntry> Entries { get { return this._entries; }set { this._entries = value; } }

    public ServiceFlowsViewModel()
    {
      this._flows = new List<TemplateServiceFlow>();
    }
  }
}