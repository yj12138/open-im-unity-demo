using GameFramework.Event;
using OpenIM.Proto;

namespace Dawn.Game.Event
{
    public enum GroupOperation
    {
        None,
        GroupApplicationAccepted,
        GroupApplicationAdded,
        GroupApplicationDeleted,
        GroupApplicationRejected,
        GroupDismissed,
        GroupInfoChanged,
        GroupMemberAdded,
        GroupMemberDeleted,
        GroupMemberInfoChanged,
        JoinedGroupAdded,
        JoinedGroupDeleted,
    }

    public class OnGroupChange : GameEventArgs
    {
        public static readonly int EventId = typeof(OnGroupChange).GetHashCode();
        public override int Id
        {
            get
            {
                return EventId;
            }
        }
        public GroupOperation Operation;
        public IMConversation OldConversation;
        public IMConversation NewConversation;
        public IMGroup Group;
        public IMGroupApplication GroupRequest;
        public IMGroupMember GroupMemeber;
        public override void Clear()
        {
            Operation = GroupOperation.None;
            OldConversation = null;
            NewConversation = null;
            Group = null;
            GroupRequest = null;
            GroupMemeber = null;
        }

        public bool IsGroupCountChange()
        {
            if (Operation == GroupOperation.GroupDismissed)
            {
                return true;
            }
            if (Operation == GroupOperation.JoinedGroupAdded)
            {
                return true;
            }
            if (Operation == GroupOperation.JoinedGroupDeleted)
            {
                return true;
            }
            return false; ;
        }
    }
}
