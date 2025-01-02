using OpenIM.IMSDK.Listener;
using OpenIM.Proto;
using UnityEngine;
namespace Dawn.Game
{
    public class MessageListener : IMessageListener
    {
        public void OnMessageDeleted(IMMessage message)
        {
            Debug.Log("OnMessageDeleted:" + message.ToString());
        }

        public void OnMessageEdited(IMMessage message)
        {
            Debug.Log("OnMessageEdited:" + message.ToString());
        }

        public void OnNewRecvMessageRevoked(RevokedTips revokedTips)
        {
            Debug.Log("OnNewRecvMessageRevoked:" + revokedTips.ToString());
        }

        public void OnRecvC2CReadReceipt(MessageReceipt[] msgReceiptList)
        {
            Debug.Log("OnRecvC2CReadReceipt:" + msgReceiptList.ToString());
        }

        public void OnRecvNewMessage(IMMessage message)
        {
            Debug.Log("OnRecvNewMessage:" + message.ToString());
        }

        public void OnRecvOfflineNewMessage(IMMessage message)
        {
            Debug.Log("OnRecvOfflineNewMessage:" + message.ToString());
        }

        public void OnRecvOnlineOnlyMessage(IMMessage message)
        {
            Debug.Log("OnRecvOnlineOnlyMessage:" + message.ToString());
        }
    }

}

