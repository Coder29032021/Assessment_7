using System;
using System.Collections.Generic;

namespace FieldAgent.Core.Entities
{
    public class Agent
    {
        public int AgentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Height { get; set; }

        public List<AgencyAgent> AgencyAgent { get; set; }
        public List<Mission> Mission { get; set; }

        public List<Alias> Alias { get; set; }

    }
}
