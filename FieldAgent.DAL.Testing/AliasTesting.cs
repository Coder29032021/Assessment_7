using NUnit.Framework;
using FieldAgent.DAL.Repositories;
using FieldAgent.DAL;
using FieldAgent.Core.Entities;
using FieldAgent.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Collections.Generic;
using System;
namespace FieldAgent.DAL.Testing
{
    public class AliasTesting
    {
        //if you are trying to have test data for reports makes sense, otherwise just use repository add additional things

        //create an insert command in sql for reports --> so you would have to insert some data into data base for ADO.net to read so write insert statements to insert o
        private FieldAgentContext db;
        private AliasRepository aliasRepo;

        public readonly static Alias ALIAS = MakeAlias();
        public readonly static Alias ALIAS1= MakeAlias1();
        public readonly static Alias ALIAS2= MakeAlias2();



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
        public void ValueForAliasShouldInsert()
        {
            aliasRepo.Insert(ALIAS);

            var response = db.Alias.Find(1);

            Assert.AreEqual(response.AliasName, ALIAS.AliasName);

        }
        [Test]
        public void ShouldDeleteAlias()
        {
            Response aResponse = new Response();
            aliasRepo.Insert(ALIAS);
            aliasRepo.Insert(ALIAS);
            aResponse = aliasRepo.Delete(1);

            Assert.IsTrue(aResponse.Success);

        }

        [Test]
        public void ShouldUpdate()
        {
            Response response = new Response();
            aliasRepo.Insert(ALIAS);

            ALIAS.AliasName = "Chan";
           response = aliasRepo.Update(ALIAS);
            Assert.IsTrue(response.Success);
        }

        [Test]
        public void GetAliasByAliasIDShouldWork()
        {
            Response<Alias> response = new Response<Alias>();
            aliasRepo.Insert(ALIAS);


            response.Data = ALIAS;
            var fromMethod = aliasRepo.Get(1);

            Assert.AreEqual(response.Data, fromMethod.Data);
        }
        [Test]
        public void GetListofAliasesByAgentID()
        {
            Response<List<Alias>> response = new Response<List<Alias>>();

            aliasRepo.Insert(ALIAS);
            aliasRepo.Insert(ALIAS1);
            aliasRepo.Insert(ALIAS2);

            response = aliasRepo.GetByAgent(1);
            Assert.AreEqual(3, response.Data.Count);
        }
        //supporting Code
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
        public static Alias MakeAlias1()
        {
            Alias alias1 = new Alias()
            {
                AgentId = 1,
                AliasName = "008",
                InterpolId = new Guid("e5682795-99c8-4fab-af40-89aeb39fca1c"),
                Persona = "Happy"
            };
            return alias1;
        }
        public static Alias MakeAlias2()
        {
            Alias alias1 = new Alias()
            {
                AgentId = 1,
                AliasName = "009",
                InterpolId = new Guid("e5982755-99c8-4fab-af40-89aeb39fca1c"),
                Persona = "Sad"
            };
            return alias1;
        }
    }
}