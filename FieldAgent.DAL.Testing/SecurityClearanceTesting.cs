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
    public class SecurityClearanceTesting : DbContext
    {
        //if you are trying to have test data for reports makes sense, otherwise just use repository add additional things

        //create an insert command in sql for reports --> so you would have to insert some data into data base for ADO.net to read so write insert statements to insert o
        private FieldAgentContext db;
        private SecurityClearanceRepository securityRepo;

        public readonly static SecurityClearance SECURITY1 = MakeSecurityClearance1();
        public readonly static SecurityClearance SECURITY2 = MakeSecurityClearance2();
        public readonly static SecurityClearance SECURITY3 = MakeSecurityClearance3();

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
            securityRepo = new SecurityClearanceRepository(db);
        }

        //[Test]
        //public void GetSecurityClearancesShouldWork()
        //{
        //    Response<Alias> response = new Response<Alias>();
        //    aliasRepo.Insert(ALIAS);


        //    response.Data = ALIAS;
        //   response.

        //    Assert.AreEqual(response.Data, fromMethod.Data);
        //}
        [Test]
        public void GetAllAliases()
        {
            Response<List<SecurityClearance>> response = new Response<List<SecurityClearance>>();

            db.Add(SECURITY1);
            SaveChanges();

            db.Add(SECURITY2);
            SaveChanges();

            db.Add(SECURITY3);
            SaveChanges();

            response = securityRepo.GetAll();
            Assert.AreEqual(3, response.Data.Count);
        }
        public static SecurityClearance MakeSecurityClearance1()
        {
            SecurityClearance securityClearance1 = new SecurityClearance()
            {
                SecurityClearanceName = "007"
            };
            return securityClearance1;
        }
        public static SecurityClearance MakeSecurityClearance2()
        {
            SecurityClearance securityClearance2 = new SecurityClearance()
            {
                SecurityClearanceName = "008"
            };
            return securityClearance2;
        }
        public static SecurityClearance MakeSecurityClearance3()
        {
            SecurityClearance securityClearance3 = new SecurityClearance()
            {
                SecurityClearanceName = "009"
            };
            return securityClearance3;
        }
    }
}