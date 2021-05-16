using NUnit.Framework;
using FieldAgent.DAL.Repositories;
using FieldAgent.DAL;
using FieldAgent.Core.Entities;
using FieldAgent.Core.DTOs;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration.CommandLine;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
namespace FieldAgent.DAL.Testing
{
    public class AgencyTesting: DbContext
    {
        private FieldAgentContext db;
        private AgentRepository repo;
        private Agency agency1 = new Agency()
        {
            ShortName = "FBI",
            LongName = "Federal Breakfast Inspectors"
        };
    private Agency agency2 = new Agency()
    {
        ShortName = "CIA",
        LongName = "Centralist Inspection Agency"
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

            db.Agency.Add(agency1);
            db.Agency.Add(agency2);
            db.SaveChanges();
            repo = new AgentRepository(db);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}