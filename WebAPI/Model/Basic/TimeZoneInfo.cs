using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Basic
{
    public class TimeZoneInformation 
    {
      public string? Id { get; set; } 
      public string? TimeZoneName { get; set; }
      public string? GMT { get; set; }
      public string? GMTValue { get; set; }
      public string? CountryId { get; set; }
      public string? SLNO { get; set; }
    } 
    public class MenuSettings
    {
      public string? Id { get; set; } 
      public string? ModuleName { get; set; }
      public string? Header { get; set; }
      public string? MenuTitle { get; set; }
      public string? MenuDescription { get; set; }
      public string? URL { get; set; }
      public string? SLNO { get; set; }
    }
}
