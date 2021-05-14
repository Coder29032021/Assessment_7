using System;
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

    public class Location
    {
        public int LocationId { get; set; }
        public int AgencyId { get; set; }
        public string LocationName { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public Agency Agency { get; set; }
    }
    public class Mission
    {
        public int MissionId { get; set; }
        public int AgencyId { get; set; }
        public string CodeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ProjectedEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public decimal OperationalCost { get; set; }
        public string Notes { get; set; }

        public Agency Agency { get; set; }
        public List<MissionAgent> MissionAgent { get; set; }
        public List<Agent> Agent { get; set; }

    }

    public class Agent
    {
        public int AgentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Height { get; set; }

        public List<AgencyAgent> AgencyAgent { get; set; }
        public List<MissionAgent> MissionAgent { get; set; }
        public List<Mission> Mission { get; set; }

        public List<Alias> Alias { get; set; }

    }

    public class Alias
    {
        public int AliasId { get; set; }
        public int AgentId { get; set; }
        public string AliasName { get; set; }
        public Guid? InterpolId { get; set; }
        public string Persona { get; set; }

        public Agent Agent { get; set; }

    }

    public class MissionAgent
    {
        public int MissionId { get; set; }
        public int AgentId { get; set; }

        public Mission Mission { get; set; }
        public Agent Agent { get; set; }
    }

    public class SecurityClearance
    {
        public int SecurityClearanceId { get; set; }
        public string SecurityClearanceName { get; set; }
        public List<AgencyAgent> AgencyAgent { get; set; }
    }

    public class AgencyAgent
    {
        public int AgencyId { get; set; }
        public int AgentId { get; set; }


        public int SecurityClearanceId { get; set; }
        public Guid BadgeId { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public int IsActive { get; set; }

        public Agency Agency { get; set; }
        public Agent Agent { get; set; }
        public SecurityClearance SecurityClearance { get; set; }

    }
}
