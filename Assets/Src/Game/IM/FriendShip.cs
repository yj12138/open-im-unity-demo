using Dawn.Game.Event;
using OpenIM.Proto;
using OpenIM.IMSDK.Listener;


namespace Dawn.Game
{
    public class FriendShipListener : IFriendShipListener
    {
        public FriendShipListener()
        {
        }
        public void OnBlackAdded(IMBlack blackInfo)
        {
            GameEntry.Event.Fire(OnFriendChange.EventId, new OnFriendChange()
            {
                Black = blackInfo,
                Operation = FriendOperation.BlackedAdd
            });
        }

        public void OnBlackDeleted(IMBlack blackInfo)
        {
            GameEntry.Event.Fire(OnFriendChange.EventId, new OnFriendChange()
            {
                Black = blackInfo,
                Operation = FriendOperation.BlackDeleted
            });
        }

        public void OnFriendAdded(IMFriend friendInfo)
        {
            GameEntry.Event.Fire(OnFriendChange.EventId, new OnFriendChange()
            {
                Friend = friendInfo,
                Operation = FriendOperation.Added
            });
        }

        public void OnFriendDeleted(IMFriend friendInfo)
        {
            GameEntry.Event.Fire(OnFriendChange.EventId, new OnFriendChange()
            {
                Friend = friendInfo,
                Operation = FriendOperation.Deleted
            });
        }

        public void OnFriendInfoChanged(IMFriend friendInfo)
        {
            GameEntry.Event.Fire(OnFriendChange.EventId, new OnFriendChange()
            {
                Friend = friendInfo,
                Operation = FriendOperation.InfoChanged
            });
        }

        public void OnFriendApplicationAccepted(IMFriendApplication friendApplication)
        {
            GameEntry.Event.Fire(OnFriendChange.EventId, new OnFriendChange()
            {
                FriendRequest = friendApplication,
                Operation = FriendOperation.ApplicationAccepted
            });
        }

        public void OnFriendApplicationAdded(IMFriendApplication friendApplication)
        {
            GameEntry.Event.Fire(OnFriendChange.EventId, new OnFriendChange()
            {
                FriendRequest = friendApplication,
                Operation = FriendOperation.ApplicationAdded
            });
        }

        public void OnFriendApplicationDeleted(IMFriendApplication friendApplication)
        {
            GameEntry.Event.Fire(OnFriendChange.EventId, new OnFriendChange()
            {
                FriendRequest = friendApplication,
                Operation = FriendOperation.ApplicationDeleted
            });
        }

        public void OnFriendApplicationRejected(IMFriendApplication friendApplication)
        {
            GameEntry.Event.Fire(OnFriendChange.EventId, new OnFriendChange()
            {
                FriendRequest = friendApplication,
                Operation = FriendOperation.ApplicationRejected
            });
        }
    }
}

