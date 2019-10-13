using System;
using System.Linq;
using System.Threading.Tasks;
using Ntl.TappyService.Tests.Data;

namespace Ntl.TappyService.Tests.Utils
{
    public static class DbHelper
    {
        private const string UnitTestVersionField = "unit_test";

        public static void RemoveTestData()
        {
            using var context = new ScoreBoardContext();
            var items = context.ScoreBoard.Where(i => i.Version == UnitTestVersionField);
            context.ScoreBoard.RemoveRange(items);
            context.SaveChanges();
        }

        public static ScoreBoardItem CreateScoreItem(string userName, string playerId, int position, DateTime dateTime)
        {
            return new ScoreBoardItem()
            {
                Created = dateTime,
                Name = userName,
                PlayerId = playerId,
                Position = position,
                Version = UnitTestVersionField
            };
        }

        public static async Task Insert(ScoreBoardItem[] items)
        {
            using var context = new ScoreBoardContext();
            await context.ScoreBoard.AddRangeAsync(items);
            await context.SaveChangesAsync();
        }
    }
}