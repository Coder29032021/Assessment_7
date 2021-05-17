using NUnit.Framework;
using FieldAgent.DAL.Repositories;
using FieldAgent.DAL;
using FieldAgent.Core.Entities;
using FieldAgent.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
namespace FieldAgent.DAL.Testing
{
    public class AgencyAgentTesting: DbContext
    {
        private FieldAgentContext db;
        private AgentRepository repo;
        private AliasRepository aliasRepo;
        private AgencyAgentRepository agencyAgentRepo;
        private MissionRepository missionRepo;

        public readonly static AgencyAgent AGENCYAGENT1 = MakeAgencyAgent1();
        public readonly static AgencyAgent AGENCYAGENT2 = MakeAgencyAgent2();




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
        public void ValueForAgentShouldInsert()
        {
            agencyAgentRepo.Insert(AGENCYAGENT1);

            var response = db.AgencyAgent.Find(1,1);
            Assert.AreEqual(response.BadgeId, AGENCYAGENT1.BadgeId);

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
                AgencyId = 1,
                AgentId = 1,
                SecurityClearanceId = 1,
                BadgeId = new Guid("3c12153d-a366-40df-a36d-bd9f99870108"),
                ActivationDate = DateTime.Parse("4/10/2008"),
                DeactivationDate = DateTime.Parse("4/20/2014"),
                IsActive = 1,
                
            };
            return agencyAgent;
        }
    }
}