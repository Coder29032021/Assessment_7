﻿using System.Collections.Generic;
using FieldAgent.Core.Entities;

namespace FieldAgent.Core.Interfaces.DAL
{
    public interface IMissionRepository
    {
        Response<Mission> Insert(Mission mission);
        Response Update(Mission mission);
        Response Delete(int missionId);
        Response<Mission> Get(int missionId);
        Response<List<Mission>> GetByAgency(int agencyId);
        Response<List<Mission>> GetByAgent(int agentId);
    }

}
