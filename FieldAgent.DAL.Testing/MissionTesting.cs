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
    public class AgentTesting : DbContext
    {
        //if you are trying to have test data for reports makes sense, otherwise just use repository add additional things

        //create an insert command in sql for reports --> so you would have to insert some data into data base for ADO.net to read so write insert statements to insert o
        private FieldAgentContext db;
        private AgentRepository repo;
        private AliasRepository aliasRepo;
        private AgencyAgentRepository agencyAgentRepo;

        public readonly static Mission MISSION1 = MakeMission1();
        public readonly static Mission MISSION2 = MakeMission2();

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
            //public Response<Agent> Get(int agentId)
            //{
            //    Response<Agent> response = new Response<Agent>();
            //    Agent agent = new Agent();
            //    _context.Agent.Find(agentId);
            //    response.Data = agent;
            //    return response;
            //}

            Response<Agent> response = new Response<Agent>();

            repo.Insert(AGENT1);


            response.Data = AGENT1;
            var fromMethod = repo.Get(1);

            Assert.AreEqual(response.Data, fromMethod.Data);
        }
        [Test]
        public void Delete()
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
        public static Mission MakeMission1()
        {


            Mission mission = new Mission()
            {


                AgencyId = 1,
                CodeName = "KidsNextDoor",
                StartDate = DateTime.Parse("03/17/2020"),
                ProjectedEndDate = DateTime.Parse("03/20/2022"),
                ActualEndDate = DateTime.Parse("04/20/2022"),
                OperationalCost = 1004.01M,
                Notes = "NigelUno"
            };
            return mission;
        }
        public static Mission MakeMission2()
        {


            Mission mission = new Mission()
            {


                AgencyId = 2,
                CodeName = "KidsNextDoor",
                StartDate = DateTime.Parse("1/17/2019"),
                ProjectedEndDate = DateTime.Parse("02/20/2022"),
                ActualEndDate = DateTime.Parse("04/20/2022"),
                OperationalCost = 1114.01M,
                Notes = "KND defeats Father"
            };
            return mission;
        }
    }
}