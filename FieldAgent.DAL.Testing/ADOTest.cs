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
using NUnit.Framework;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interfaces.DAL;
using Microsoft.EntityFrameworkCore;

namespace FieldAgent.DAL.Testing
{
    public class ADOTest
    {
        //if you are trying to have test data for reports makes sense, otherwise just use repository add additional things

        //create an insert command in sql for reports --> so you would have to insert some data into data base for ADO.net to read so write insert statements to insert o
        private FieldAgentContext db;
        private ReportsRepository reportRepo;

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
            reportRepo = new ReportsRepository(FieldAgentContext.GetConnectionString());
        }

        [Test]
        public void shouldreturnTopAgent()
        {
            var result = reportRepo.GetTopAgents();

            Assert.AreEqual(2, result.Data.Count);
        }
        [Test]
        public void ShoudlReturnPensionList()
        {
            var result = reportRepo.GetPensionList(1);
            Assert.AreEqual(2, result.Data.Count);
        }
        [Test]
        public void ShouldReturnClearanceAudit()
        {
            var result = reportRepo.AuditClearance(1, 1);
            Assert.AreEqual(2, result.Data.Count);
        }
    }
}