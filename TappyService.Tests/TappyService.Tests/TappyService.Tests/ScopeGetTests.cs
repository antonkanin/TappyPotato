using System;
using System.Linq;
using Ntl.TappyService.Tests.Data;
using NUnit.Framework;

namespace Ntl.TappyService.Tests
{
    public class ScopeGetTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            InsertData();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            DeleteData();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        private static void InsertData()
        {
            using (var context = new ScoreBoardContext())
            {
                var newItem = new ScoreBoardItem
                {
                    Created = DateTime.Now,
                    Name = "NewUser",
                    PlayerId = "newuserid",
                    Position = 42,
                    Score = 10,
                    Version = "2.2.1"
                };
                context.ScoreBoard.Add(newItem);
                context.SaveChanges();
            }
        }

        private static void DeleteData()
        {
            using (var context = new ScoreBoardContext())
            {
                var items = context.ScoreBoard.Where(i => i.PlayerId == "newuserid");
                foreach (var item in items)
                {
                    context.ScoreBoard.Remove(item);
                }

                context.SaveChanges();
            }
        }
    }
}