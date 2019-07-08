using System;
using System.ComponentModel.DataAnnotations.Schema;
using MySqlX.XDevAPI.Relational;

namespace Ntl.TappyService.Tests.Data
{
    public class ScoreBoardItem
    {
        [Column("number")]
        public long Number { get; set; }
        
        [Column("player_id")]
        public string PlayerId { get; set; }
        
        [Column("player_name")]
        public string Name { get; set; }
        
        [Column("score")]
        public int Score { get; set; }
        
        [Column("death_position")]
        public int Position { get; set; }
        
        [Column("date_created")]
        public DateTime Created { get; set; }
        
        [Column("version")]
        public string Version { get; set; }
    }
}