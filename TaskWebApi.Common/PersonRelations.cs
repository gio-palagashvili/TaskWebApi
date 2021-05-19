// ReSharper disable all UnusedMember.Local

using System.Collections.Generic;

namespace TaskWebApi
{
    public class PersonRelations
    {
        public string Id { get; set; }
        public string PersonId { get; set; }
        public string RelationId { get; set; }
        public string RelationType { get; set; }
        // public List<RelationType> RelationType { get; set; }
    }
    // public enum RelationType
    // {
    //     Acquaintance = 0,
    //     Colleague = 1,
    //     Raltive = 2,
    //     Other = 3
    // }
}