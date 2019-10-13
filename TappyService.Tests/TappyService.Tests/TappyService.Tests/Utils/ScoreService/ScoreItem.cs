using System.Runtime.Serialization;

namespace Ntl.TappyService.Tests.Utils.ScoreService
{
    [DataContract(Name = "get_scores")]
    public class ScoreItem
    {
        [DataMember(Name = "player_name")]
        public string PlayerName { get; set; }
    }
}