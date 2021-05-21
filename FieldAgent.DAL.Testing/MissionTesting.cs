using NUnit.Framework;
using FieldAgent.DAL.Repositories;
using System.Collections.Generic;
using FieldAgent.Core.Entities;
using FieldAgent.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
namespace FieldAgent.DAL.Testing
{
    public class MissionTesting
    {
        //if you are trying to have test data for reports makes sense, otherwise just use repository add additional things

        //create an insert command in sql for reports --> so you would have to insert some data into data base for ADO.net to read so write insert statements to insert o
        private FieldAgentContext db;
        private AgentRepository repo;
        private AliasRepository aliasRepo;
        private AgencyAgentRepository agencyAgentRepo;
        private MissionRepository missionRepo;

        public readonly static Mission MISSION1 = MakeMission1();
        public readonly static Mission MISSION2 = MakeMission2();
        public readonly static Mission MISSION3 = MakeMission3();


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
        public void ShouldDeleteMission()
        {
            Response aResponse = new Response();
            missionRepo.Insert(MISSION1);
            missionRepo.Insert(MISSION2);
            aResponse = missionRepo.Delete(1);

            Assert.IsTrue(aResponse.Success);

        }
        [Test]
        public void GetMissionByMissionID()
        {
            Response<Mission> response = new Response<Mission>();
            missionRepo.Insert(MISSION1);


            response.Data = MISSION1;
            var fromMethod = missionRepo.Get(1);

            Assert.AreEqual(response.Data, fromMethod.Data);
        }
        [Test]
        public void UsingAgencyIdGetListOfMissions()
        {
            Response<List<Mission>> response = new Response<List<Mission>>();


            Mission mission = MISSION1;
            mission.Agency = AgencyTesting.AGENCY1;

            Mission mission1 = MISSION2;
            mission1.Agency = AgencyTesting.AGENCY1;

            missionRepo.Insert(mission);
            missionRepo.Insert(mission1);

            response = missionRepo.GetByAgency(1);
            Assert.AreEqual(2, response.Data.Count);
        }
        [Test]
        public void UsingAgentIdGetListOfMissions()
        {
            Response<List<Mission>> response = new Response<List<Mission>>();


            Mission mission = MissionTesting.MISSION1;
            mission.Agent = new List<Agent>();
            mission.Agent.Add(AgentTesting.AGENT1);

            Mission mission1 = MissionTesting.MISSION2;
            mission1.Agent = new List<Agent>();
            mission1.Agent.Add(AgentTesting.AGENT1);

            missionRepo.Insert(mission);
            missionRepo.Insert(mission1);

            response = repo.GetMissions(1);
            Assert.AreEqual(2, response.Data.Count);
        }

        [Test]
        public void ValueForMissionShouldInsert()
        {
            missionRepo.Insert(MISSION3);

            var response = db.Mission.Find(1);
            Assert.AreEqual(response.CodeName, "GruFromDespicableMe");

        }
        [Test]

        public void UpdatingMission()
        {
            Response response = new Response();
            var MissionToUpdate = MISSION1;
            missionRepo.Insert(MissionToUpdate);

            MissionToUpdate.CodeName = "Ghost";
            response = missionRepo.Update(MissionToUpdate);

            Assert.IsTrue(response.Success);
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


                AgencyId = 1,
                CodeName = "Jungler",
                StartDate = DateTime.Parse("1/17/2019"),
                ProjectedEndDate = DateTime.Parse("02/20/2022"),
                ActualEndDate = DateTime.Parse("04/20/2022"),
                OperationalCost = 1114.01M,
                Notes = "KND defeats Father"

            };
            return mission;
        }
        public static Mission MakeMission3()
        {


            Mission mission = new Mission()
            {


                AgencyId = 3,
                CodeName = "GruFromDespicableMe",
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