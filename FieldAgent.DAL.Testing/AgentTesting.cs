using NUnit.Framework;
using FieldAgent.DAL.Repositories;
using FieldAgent.DAL;
using FieldAgent.Core.Entities;
using FieldAgent.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
namespace FieldAgent.DAL.Testing
{
    public class AgentTesting
    {
        //if you are trying to have test data for reports makes sense, otherwise just use repository add additional things

        //create an insert command in sql for reports --> so you would have to insert some data into data base for ADO.net to read so write insert statements to insert o
        private FieldAgentContext db;
        private AgentRepository repo;
        private AliasRepository aliasRepo;
        private AgencyAgentRepository agencyAgentRepo;
        private MissionRepository missionRepo;

        public readonly static Agent AGENT1 = MakeAgent1();
        public readonly static Agent AGENT2 = MakeAgent2();

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
            repo.Insert(AGENT1);

            var response = db.Agent.Find(1);
            Assert.AreEqual(response.FirstName, AGENT1.FirstName);

        }
        [Test]
        public void GetAgentShouldWork()
        {
            Response<Agent> response = new Response<Agent>();
            repo.Insert(AGENT1);


            response.Data = AGENT1;
            var fromMethod = repo.Get(1);

            Assert.AreEqual(response.Data, fromMethod.Data);
        }
        [Test]
        public void DeleteingDependencyAndAgentObject()
        {

            Response aResponse = new Response();
            
            repo.Insert(AGENT1);
            
            Alias alias = AliasTesting.ALIAS;
            aliasRepo.Insert(alias);

            AgencyAgent agencyAgent = AgencyAgentTesting.AGENCYAGENT1;
            agencyAgentRepo.Insert(agencyAgent);

            aResponse = repo.Delete(1);

            Assert.IsTrue(aResponse.Success);

        }

        [Test]
        public void ShouldReturnListofMissions()
        {
            Response<List<Mission>> response = new Response<List<Mission>>();

            
            Mission stuff = MissionTesting.MISSION1;
            stuff.Agent = new List<Agent>();
            stuff.Agent.Add(AGENT1);
            
            Mission stuff1 = MissionTesting.MISSION2;
            stuff1.Agent = new List<Agent>(); 
            stuff1.Agent.Add(AGENT1);

            missionRepo.Insert(stuff);
            missionRepo.Insert(stuff1);

            response = repo.GetMissions(1);
            Assert.AreEqual(2, response.Data.Count);
        }
        [Test]

        public void UpdatingInsert ()
        {
            Response response = new Response();
            repo.Insert(AGENT1);
            AGENT1.FirstName = "Chan";
            response = repo.Update(AGENT1);
            Assert.AreEqual("Chan",AGENT1.FirstName);
        }
        public static Agent MakeAgent1()
        {


         Agent agent1 = new Agent()
        {

            FirstName = "Matthew",
            LastName = "Lantin",
            DateOfBirth = DateTime.Parse("03/17/1996"),
            Height = 42.0M
        };
            return agent1;
    }
        public static Agent MakeAgent2()
        {
            Agent agent2 = new Agent()
            {

                FirstName = "Alexa",
                LastName = "Luca",
                DateOfBirth = DateTime.Parse("12/01/1999"),
                Height = 30.0M
            };
            return agent2;
        }
    }
}