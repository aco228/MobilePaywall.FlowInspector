using MobilePaywall.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobilePaywall.FlowInspector.DataSet
{
  public class CountryTemplateModel
  {

    private Country _country = null;
    private List<MerchantTemplateModel> _merchants = null;

    public int ID { get { return this._country.ID; } }
    public string TwoLetterIsoCode { get { return this._country.TwoLetterIsoCode; } }
    public string Name { get { return this._country.GlobalName; } }
    public List<MerchantTemplateModel> Merchants { get { return this._merchants; } set { this._merchants = value; } }

    public MerchantTemplateModel this[int merchantID]
    {
      get
      {
        return (from m in this._merchants where m.ID == merchantID select m).FirstOrDefault();
      }
    }



    public CountryTemplateModel(Country country)
    {
      this._country = country;
      this._merchants = new List<MerchantTemplateModel>();
    }

  }
}