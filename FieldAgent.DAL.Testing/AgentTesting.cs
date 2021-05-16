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
using System;
namespace FieldAgent.DAL.Testing
{
    public class AgentTesting: DbContext
    {
        //if you are trying to have test data for reports makes sense, otherwise just use repository add additional things

        //create an insert command in sql for reports --> so you would have to insert some data into data base for ADO.net to read so write insert statements to insert o
        private FieldAgentContext db;
        private AgentRepository repo;

        private Agent agent1 = new Agent()
        {

            FirstName = "Matthew",
            LastName = "Lantin",
            DateOfBirth = DateTime.Parse("03/17/1996"),
            Height = 42.0M
        };
        private Agent agent2 = new Agent()
        {

            FirstName = "Alexa",
            LastName = "Luca",
            DateOfBirth = DateTime.Parse("12/01/1999"),
            Height = 30.0M
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
            repo = new AgentRepository(db);
        }

        [Test]
        public void ValueForAgentShouldInsert()
        {
            repo.Insert(agent1);

            var response = db.Agent.Find(1);

            Assert.AreEqual(response.FirstName, agent1.FirstName);

        }
    }
}