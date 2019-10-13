using System;
using System.Linq;
using System.Threading.Tasks;
using Ntl.TappyService.Tests.Data;
using Ntl.TappyService.Tests.Utils;
using Ntl.TappyService.Tests.Utils.ScoreService;
using NUnit.Framework;

namespace Ntl.TappyService.Tests
{
    public class ScopeGetTests
    {
        private ScoreServiceClient _client;
        
        [OneTimeSetUp]
        public void Setup()
        {
            _client = new ScoreServiceClient("http://tappypotato.lan");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            DbHelper.RemoveTestData();
        }

        [Test]
        public async Task ScopeGet_returns_the_latest_death_for_a_given_position()
        {
            //arrange
            ScoreBoardItem[] items = new[]
            {
                DbHelper.CreateScoreItem("User1", "user1", 100, DateTime.Parse("2010-01-02 13:29"))
            };
            await DbHelper.Insert(items);
            
            //act
            var response = await _client.GetScore();
            
            //assert
            Assert.NotNull(response);
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
    }
}