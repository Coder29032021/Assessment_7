using NUnit.Framework;
using FieldAgent.DAL.Repositories;
using FieldAgent.Core.Entities;
using FieldAgent.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
namespace FieldAgent.DAL.Testing
{
    public class AgencyTesting
    {
        private FieldAgentContext db;
        private AgencyRepository agencyRepo;
        private MissionRepository missionRepo;
        private AgencyAgentRepository agencyagentRepo;

        public readonly static Agency AGENCY1 = MakeAgency1();
        public readonly static Agency AGENCY2 = MakeAgency2();
        public readonly static Agency AGENCY3 = MakeAgency3();




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
            agencyRepo = new AgencyRepository(db);
            agencyagentRepo = new AgencyAgentRepository(db);
            missionRepo = new MissionRepository(db);

        }

        [Test]
        public void DeletingAgencyAndDepedencies()
        {

            Response aResponse = new Response();

            agencyRepo.Insert(AGENCY1);

            AgencyAgent agencyAgent= AgencyAgentTesting.AGENCYAGENT1;
            agencyagentRepo.Insert(agencyAgent);

            Mission mission = MissionTesting.MISSION1;
            missionRepo.Insert(mission);

            aResponse = agencyRepo.Delete(1);

            Assert.IsTrue(aResponse.Success);

        }
        public static Agency MakeAgency1()
        {
            Agency agency1 = new Agency()
            {
                ShortName = "FBI",
                LongName = "Federal Breakfast Inspectors"
            };
            return agency1;
        }
        public static Agency MakeAgency2()
        {
            Agency agency2 = new Agency()
            {
                ShortName = "CIA",
                LongName = "Centralist Inspection Agency"
            };
            return agency2;
        }
        public static Agency MakeAgency3()
        {
            Agency agency3 = new Agency()
            {
                ShortName = "CDI",
                LongName = "Central Dogs Intelligence"
            };
            return agency3;
        }
    }
}