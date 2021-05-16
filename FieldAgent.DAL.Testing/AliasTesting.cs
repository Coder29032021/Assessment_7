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
    public class AliasTesting : DbContext
    {
        //if you are trying to have test data for reports makes sense, otherwise just use repository add additional things

        //create an insert command in sql for reports --> so you would have to insert some data into data base for ADO.net to read so write insert statements to insert o
        private FieldAgentContext db;
        private AgentRepository repo;
        private AliasRepository aliasRepo;
        private AgencyAgentRepository agencyAgentRepo;

        public readonly static Alias ALIAS = MakeAlias();

        private Alias alias1 = new Alias()
        {
            AgentId = 1,
            AliasName = "007",
            InterpolId = new Guid("e5682755-99c8-4fab-af40-89aeb39fca1c"),
            Persona = "Crazy"
        };

        private Alias alias2 = new Alias()
        {
            AgentId = 2,
            AliasName = "008",
            InterpolId = new Guid("e5682766-99c8-4fab-af40-89aeb39fca1c"),
            Persona = "Sane"
        };


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
            aliasRepo = new AliasRepository(db);
        }

        [Test]
        public void ValueForAgentShouldInsert()
        {
            aliasRepo.Insert(alias1);

            var response = db.Alias.Find(1);

            Assert.AreEqual(response.AliasName, alias1.AliasName);

        }
        public static Alias MakeAlias()
        {
            Alias alias1 = new Alias()
            {
                AgentId = 1,
                AliasName = "007",
                InterpolId = new Guid("e5682755-99c8-4fab-af40-89aeb39fca1c"),
                Persona = "Crazy"
            };
            return alias1;
        }
        //[Test]
        //public void GetAgentShouldWork()
        //{
        //    //public Response<Agent> Get(int agentId)
        //    //{
        //    //    Response<Agent> response = new Response<Agent>();
        //    //    Agent agent = new Agent();
        //    //    _context.Agent.Find(agentId);
        //    //    response.Data = agent;
        //    //    return response;
        //    //}

        //    Response<Agent> response = new Response<Agent>();

        //    repo.Insert(agent1);
        //    repo.Insert(agent2);

        //    response.Data = agent1;
        //    var fromMethod = repo.Get(1);

        //    Assert.AreEqual(response.Data, fromMethod.Data);
        //}
        //[Test]
        //public void Delete()
        //{
        //    Response aResponse = new Response();

        //    repo.Insert(agent1);
        //    repo.Insert(agent2);


        //    aResponse = repo.Delete(1);
        //    Assert.IsTrue(aResponse.Success);

        //}
    }
}