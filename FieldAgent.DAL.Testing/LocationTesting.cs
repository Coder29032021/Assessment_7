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
    public class LocationTesting
    {
        private FieldAgentContext db;
        private LocationRepository locationRepo;

        public readonly static Location LOCATION1 = MakeLocation1();
        public readonly static Location LOCATION2= MakeLocation2();
        public readonly static Location LOCATION3= MakeLocation3();



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
            locationRepo = new LocationRepository(db);
        }

        [Test]
        public void ValueForLocationShouldInsert()
        {
            locationRepo.Insert(LOCATION1);

            var response = db.Location.Find(1);

            Assert.AreEqual(response.LocationName, LOCATION1.LocationName);

        }
        [Test]
        public void ShouldDeleteLocation()
        {
            Response aResponse = new Response();
            locationRepo.Insert(LOCATION1);

            aResponse = locationRepo.Delete(1);

            Assert.IsTrue(aResponse.Success);

        }

        [Test]
        public void GetLocationByLocationID()
        {
            Response<Location> response = new Response<Location>();
            locationRepo.Insert(LOCATION1);


            response.Data = LOCATION1;
            var fromMethod = locationRepo.Get(1);

            Assert.AreEqual(response.Data, fromMethod.Data);
        }
        [Test]
        public void GetListofAliasesByAgencyID()
        {
            Response<List<Location>> response = new Response<List<Location>>();

            locationRepo.Insert(LOCATION1);
            locationRepo.Insert(LOCATION2);
            locationRepo.Insert(LOCATION3);

            response = locationRepo.GetByAgency(1);
            Assert.AreEqual(2, response.Data.Count);
        }

        [Test]
        public void LocationShouldUpdate()
        {
            Response response = new Response();
            var locationToUpdate = LOCATION1;
            locationRepo.Insert(locationToUpdate);

            locationToUpdate.LocationName = "Ghost";
            response = locationRepo.Update(locationToUpdate);

            Assert.IsTrue(response.Success);
        }
        //supporting Code
        public static Location MakeLocation1()
        {
            Location location = new Location()
            {

                AgencyId = 1,
                LocationName = "PromisedNeverLand",
                Street1 = "Harry",
                Street2 = "Potter",
                City = "Hogwarts",
                PostalCode = "11111-1111",
                CountryCode = "1234"
    };
            return location;
        }
        public static Location MakeLocation2()
        {
            Location location = new Location()
            {
                AgencyId = 2,
                LocationName = "GravityFalls",
                Street1 = "Grace",
                Street2 = "Potter",
                City = "Distance",
                PostalCode = "11111-1111",
                CountryCode = "1234"
            };
            return location;
        }
        public static Location MakeLocation3()
        {
            Location location = new Location()
            {
                AgencyId = 1,
                LocationName = "SummonersRift",
                Street1 = "Miss",
                Street2 = "Fortune",
                City = "R-Ability",
                PostalCode = "11111-1111",
                CountryCode = "1234"
            };
            return location;
        }
    }
}