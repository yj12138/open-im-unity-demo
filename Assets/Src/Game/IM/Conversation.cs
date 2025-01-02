using System.Collections.Generic;
using Dawn.Game.Event;
using OpenIM.Proto;
using OpenIM.IMSDK.Listener;

namespace Dawn.Game
{
    public class ConversationListener : IConversationListener
    {
        public ConversationListener()
        {
        }

        public void OnConversationChanged(IMConversation[] conversationList)
        {
            if (conversationList != null)
            {
                foreach (var conversation in conversationList)
                {
                    GameEntry.Event.Fire(OnConversationChange.EventId, new OnConversationChange()
                    {
                        Conversation = conversation,
                        Created = false,
                    });
                }
            }
        }

        public void OnNewConversation(IMConversation[] conversationList)
        {
            if (conversationList != null)
            {
                foreach (var conversation in conversationList)
                {
                    GameEntry.Event.Fire(OnConversationChange.EventId, new OnConversationChange()
                    {
                        Conversation = conversation,
                        Created = true,
                    });
                }
            }
        }
        public void OnTotalUnreadMessageCountChanged(int totalUnreadCount)
        {
            GameEntry.Event.Fire(OnConversationChange.EventId, new OnConversationChange()
            {
                IsTotalUnReadChanged = true,
            });
        }

        public void OnSyncServerProgress(int progress)
        {
            // TODO
        }

        public void OnSyncServerStart(bool reinstalled)
        {
            GameEntry.Event.Fire(OnConversationChange.EventId, new OnConversationChange()
            {
                SyncServerStatus = SyncServerStatus.Start
            });
        }

        public void OnSyncServerFinish(bool reinstalled)
        {
            GameEntry.Event.Fire(OnConversationChange.EventId, new OnConversationChange()
            {
                SyncServerStatus = SyncServerStatus.Finish
            });
        }

        public void OnSyncServerFailed(bool reinstalled)
        {
            GameEntry.Event.Fire(OnConversationChange.EventId, new OnConversationChange()
            {
                SyncServerStatus = SyncServerStatus.Failed
            });
        }

        public void OnConversationUserInputStatusChanged(string conversationId, string userId, Platform[] platforms)
        {
        }
    }
}

