using NUnit.Framework;
using FieldAgent.DAL.Repositories;
using FieldAgent.Core.Entities;
using FieldAgent.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
namespace FieldAgent.DAL.Testing
{
    public class AgencyAgentTesting
    {
        private FieldAgentContext db;
        private AgentRepository repo;
        private AliasRepository aliasRepo;
        private AgencyAgentRepository agencyAgentRepo;
        private MissionRepository missionRepo;

        public readonly static AgencyAgent AGENCYAGENT1 = MakeAgencyAgent1();
        public readonly static AgencyAgent AGENCYAGENT2 = MakeAgencyAgent2();
        public readonly static AgencyAgent AGENCYAGENT3 = MakeAgencyAgent3();

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<FieldAgentContext>()
                  .UseInMemoryDatabase("testDatabase")
                  .Options;
            db = new FieldAgentContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.SaveChanges();
            repo = new AgentRepository(db);
            aliasRepo = new AliasRepository(db);
            agencyAgentRepo = new AgencyAgentRepository(db);
            missionRepo = new MissionRepository(db);
        }
        [Test]
        public void GetListOfAgencyAgentsByAgentID()
        {
            Response<List<AgencyAgent>> response = new Response<List<AgencyAgent>>();

            agencyAgentRepo.Insert(AGENCYAGENT1);
            agencyAgentRepo.Insert(AGENCYAGENT2);

            response = agencyAgentRepo.GetByAgent(1);
            Assert.AreEqual(2, response.Data.Count);
        }
        [Test]
        public void GetListOfAgencyAgentsByAgencyID()
        {
            Response<List<AgencyAgent>> response = new Response<List<AgencyAgent>>();

            agencyAgentRepo.Insert(AGENCYAGENT1);
            agencyAgentRepo.Insert(AGENCYAGENT3);

            response = agencyAgentRepo.GetByAgency(1);
            Assert.AreEqual(2, response.Data.Count);
        }

        [Test]
        public void ShouldReadAgencyAgentByKeys()
        {
            agencyAgentRepo.Insert(AGENCYAGENT1);

            var response = db.AgencyAgent.Find(1,1);

            Assert.AreEqual(response.BadgeId, AGENCYAGENT1.BadgeId);
        }

        [Test]
        public void AgencyAgentShouldDelete()
        {
            Response aResponse = new Response();

            agencyAgentRepo.Insert(AGENCYAGENT1);

            aResponse = agencyAgentRepo.Delete(1, 1);

            Assert.IsTrue(aResponse.Success);

        }
        [Test]
        public void AgencyAgentShouldInsert()
        {
            agencyAgentRepo.Insert(AGENCYAGENT1);

            var response = db.AgencyAgent.Find(1,1);
            Assert.AreEqual(response.BadgeId, AGENCYAGENT1.BadgeId);

        }
        [Test]
        public void ShouldUpdateAgencyAgent()
        {
            Response response = new Response();
           var agencyAgentToUpdate = AGENCYAGENT1;
            agencyAgentRepo.Insert(agencyAgentToUpdate);

            agencyAgentToUpdate.IsActive= 0;
            response = agencyAgentRepo.Update(agencyAgentToUpdate);

            Assert.IsTrue(response.Success);
        }

        public static AgencyAgent MakeAgencyAgent1()
        {

         AgencyAgent agencyAgent = new AgencyAgent()
        {
            AgencyId = 1,
            AgentId = 1,
            SecurityClearanceId = 1,
            BadgeId = new Guid("3c12153d-a366-40df-a36d-bd9f99870108"),
            ActivationDate = DateTime.Parse("4/10/2008"),
            DeactivationDate = DateTime.Parse("4/20/2014"),
            IsActive = 1

        };
            return agencyAgent;
        }
        public static AgencyAgent MakeAgencyAgent2()
        {
            AgencyAgent agencyAgent = new AgencyAgent()
            {
                AgencyId = 2,
                AgentId = 1,
                SecurityClearanceId = 1,
                BadgeId = new Guid("3c12153d-b366-40df-a36d-bd9f99870108"),
                ActivationDate = DateTime.Parse("4/11/2008"),
                DeactivationDate = DateTime.Parse("4/21/2014"),
                IsActive = 1,
                
            };
            return agencyAgent;
        }
        public static AgencyAgent MakeAgencyAgent3()
        {
            AgencyAgent agencyAgent = new AgencyAgent()
            {
                AgencyId = 1,
                AgentId = 4,
                SecurityClearanceId = 1,
                BadgeId = new Guid("3c12153d-b366-40df-a36d-bd9f99870108"),
                ActivationDate = DateTime.Parse("4/11/2008"),
                DeactivationDate = DateTime.Parse("4/21/2014"),
                IsActive = 1,

            };
            return agencyAgent;
        }
    }
}