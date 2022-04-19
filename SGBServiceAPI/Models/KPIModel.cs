using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Models
{
    public class KPIModel
    {
      public int Id { get; set; }
      public string  AreaOfEvaulation { get; set; }
      public string Component { get; set; }
      public string Kpi { get; set; }
      public string OptionalCompulsory { get; set; }
      public string Rating { get; set; }
      public string Legislation { get; set; }
      public string Description { get; set; }
      public string BusinessUnit { get; set; }
      public string Resource { get; set; }
      public string KpiAuditTrail { get; set; }
    }
}
