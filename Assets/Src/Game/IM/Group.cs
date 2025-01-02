using Dawn.Game.Event;
using OpenIM.Proto;
using OpenIM.IMSDK.Listener;


namespace Dawn.Game
{
    public class GroupListener : IGroupListener
    {
        public GroupListener()
        {
        }

        public void OnGroupApplicationAccepted(IMGroupApplication groupApplication)
        {
            GameEntry.Event.Fire(OnGroupChange.EventId, new OnGroupChange()
            {
                Operation = GroupOperation.GroupApplicationAccepted,
                GroupRequest = groupApplication,
            });
        }

        public void OnGroupApplicationAdded(IMGroupApplication groupApplication)
        {
            GameEntry.Event.Fire(OnGroupChange.EventId, new OnGroupChange()
            {
                Operation = GroupOperation.GroupApplicationAdded,
                GroupRequest = groupApplication,
            });
        }

        public void OnGroupApplicationDeleted(IMGroupApplication groupApplication)
        {
            GameEntry.Event.Fire(OnGroupChange.EventId, new OnGroupChange()
            {
                Operation = GroupOperation.GroupApplicationDeleted,
                GroupRequest = groupApplication,
            });
        }

        public void OnGroupApplicationRejected(IMGroupApplication groupApplication)
        {
            GameEntry.Event.Fire(OnGroupChange.EventId, new OnGroupChange()
            {
                Operation = GroupOperation.GroupApplicationRejected,
                GroupRequest = groupApplication,
            });
        }

        public void OnGroupDismissed(IMGroup groupInfo)
        {
            GameEntry.Event.Fire(OnGroupChange.EventId, new OnGroupChange()
            {
                Operation = GroupOperation.GroupDismissed,
                Group = groupInfo,
            });
        }

        public void OnGroupInfoChanged(IMGroup groupInfo)
        {
            GameEntry.Event.Fire(OnGroupChange.EventId, new OnGroupChange()
            {
                Operation = GroupOperation.GroupInfoChanged,
                Group = groupInfo,
            });
        }

        public void OnGroupMemberAdded(IMGroupMember groupMemberInfo)
        {
            GameEntry.Event.Fire(OnGroupChange.EventId, new OnGroupChange()
            {
                Operation = GroupOperation.GroupMemberAdded,
                GroupMemeber = groupMemberInfo,
            });
        }

        public void OnGroupMemberDeleted(IMGroupMember groupMemberInfo)
        {
            GameEntry.Event.Fire(OnGroupChange.EventId, new OnGroupChange()
            {
                Operation = GroupOperation.GroupMemberDeleted,
                GroupMemeber = groupMemberInfo,
            });
        }

        public void OnGroupMemberInfoChanged(IMGroupMember groupMemberInfo)
        {
            GameEntry.Event.Fire(OnGroupChange.EventId, new OnGroupChange()
            {
                Operation = GroupOperation.GroupMemberInfoChanged,
                GroupMemeber = groupMemberInfo,
            });
        }

        public void OnJoinedGroupAdded(IMGroup groupInfo)
        {
            GameEntry.Event.Fire(OnGroupChange.EventId, new OnGroupChange()
            {
                Operation = GroupOperation.JoinedGroupAdded,
                Group = groupInfo,
            });
        }

        public void OnJoinedGroupDeleted(IMGroup groupInfo)
        {
            GameEntry.Event.Fire(OnGroupChange.EventId, new OnGroupChange()
            {
                Operation = GroupOperation.JoinedGroupDeleted,
                Group = groupInfo,
            });
        }
    }
}

