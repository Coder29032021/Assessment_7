using System.Collections.Generic;

namespace FieldAgent.Core.Entities
{
    public class SecurityClearance
    {
        public int SecurityClearanceId { get; set; }
        public string SecurityClearanceName { get; set; }
        public List<AgencyAgent> AgencyAgent { get; set; }
    }
}
