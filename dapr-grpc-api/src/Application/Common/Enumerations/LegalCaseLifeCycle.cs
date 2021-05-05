using SC.API.CleanArchitecture.Domain.Enums;
using System.Linq;

namespace SC.API.CleanArchitecture.Application.Common.Enumerations
{
    public class TicketLifeCycle : Enumeration
    {
        public static TicketLifeCycle Created = new TicketLifeCycle(1, "Created", LifeCycleStateTypeEnum.Initial);
        public static TicketLifeCycle Received = new TicketLifeCycle(2, "Received", LifeCycleStateTypeEnum.Initial);
        public static TicketLifeCycle Delegated = new TicketLifeCycle(3, "Delegated", LifeCycleStateTypeEnum.Transition);
        public static TicketLifeCycle OpenedByMistake = new TicketLifeCycle(4, "Opened by mistake", LifeCycleStateTypeEnum.SemiFinal);
        public static TicketLifeCycle Archived = new TicketLifeCycle(5, "Archived", LifeCycleStateTypeEnum.Final);
        public static TicketLifeCycle Original = new TicketLifeCycle(6, "Original", LifeCycleStateTypeEnum.Final);
        public static TicketLifeCycle Aggregated = new TicketLifeCycle(7, "Aggregated", LifeCycleStateTypeEnum.SemiFinal);
        public static TicketLifeCycle Suspended = new TicketLifeCycle(8, "Suspended", LifeCycleStateTypeEnum.Transition);
        public static TicketLifeCycle Excluded = new TicketLifeCycle(9, "Excluded", LifeCycleStateTypeEnum.Final);
        public static TicketLifeCycle Issued = new TicketLifeCycle(10, "Issued", LifeCycleStateTypeEnum.Final);
        public static TicketLifeCycle Unavailable = new TicketLifeCycle(11, "Unavailable", LifeCycleStateTypeEnum.Final);

        public LifeCycleStateTypeEnum LifeCycleStateType { get; }
        public LifeCycleClassDefinitionEnum LifeCycleClassDefinition { get; } = LifeCycleClassDefinitionEnum.TicketLifeCycle;


        public TicketLifeCycle(int id, string name, LifeCycleStateTypeEnum lifeCycleStateType)
            : base(id, name)
        {
            LifeCycleStateType = lifeCycleStateType;
        }

        public static TicketLifeCycle GetInitial()
        {
            return GetAll<TicketLifeCycle>().First(t => t.LifeCycleStateType == LifeCycleStateTypeEnum.Initial);
        }

        //public static IEnumerable<RequestLifeCycle> List()
        //{
        //    return new[] { Created, InProcess, InModification, Declined, Approved, ApprovalDispatched, Revoked, Realized };
        //}
    }
}
