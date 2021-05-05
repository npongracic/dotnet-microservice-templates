using SC.API.CleanArchitecture.Domain;
using System.Collections.Generic;

namespace SC.API.CleanArchitecture.Application.Common
{
    public class SecurityObject
    {
        public SecurityObject()
        {
            this.Involvements = new Dictionary<int, List<long>>();
            this.Operations = new List<OperationEnum>();
            this.AllowedServiceProviders = new List<long>();
        }

        //EntityID i EntityOwnerId ide van
        //public long EntityId { get; set; }  //Id Entity-ja 
        //public long EntityOwnerId { get; set; } //PartyRoleId organizacije koja je owner tog Entity-ja
        public Dictionary<int, List<long>> Involvements { get; set; } // key: InvolvementRoleTypeId ; value: list PartyRoleId-jeva koji su u tom InvolvementRoleType-u u interakciji s tim entitetom
        public List<OperationEnum> Operations { get; set; }
        public List<long> AllowedServiceProviders { get; set; }
    }
}
