using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using OpenIM.Proto;
using UnityGameFramework.Runtime;
namespace Dawn.Game.Event
{
    public enum SyncServerStatus
    {
        Empty, Start, Failed, Finish
    }

    public class OnConversationChange : GameEventArgs
    {
        public static readonly int EventId = typeof(OnConversationChange).GetHashCode();
        public override int Id
        {
            get
            {
                return EventId;
            }
        }
        public bool Created;
        public bool ClearHistory;
        public IMConversation Conversation;
        public SyncServerStatus SyncServerStatus = SyncServerStatus.Empty;
        public bool IsTotalUnReadChanged;
        public override void Clear()
        {
            Conversation = null;
            SyncServerStatus = SyncServerStatus.Empty;
            Created = false;
            ClearHistory = false;
            IsTotalUnReadChanged = false;
        }
    }
}
