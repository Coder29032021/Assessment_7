using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.Core.Entities
{

    public class Agency
    {
        public int AgencyId { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }

        public List<AgencyAgent> AgencyAgent { get; set; }
        public List<Location> Location { get; set; }
        public List<Mission> Mission { get; set; }

    }
}
