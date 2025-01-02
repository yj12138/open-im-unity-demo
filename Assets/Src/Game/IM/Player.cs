using OpenIM.IMSDK;
using OpenIM.Proto;
using UnityEngine;

namespace Dawn.Game
{
    public enum UserStatus
    {
        NoLogin, Logining, LoginSuc, LoginFailed
    }
    public enum ConnStatus
    {
        Empty, OnConnecting, ConnSuc, ConnFailed, KickOffline, TokenExpired
    }
    public class Player
    {
        static Player instance;
        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Player();
                }
                return instance;
            }
        }
        public string UserId = "";
        public string Token = "";
        public UserStatus Status = UserStatus.NoLogin;
        public ConnListener conn;
        public ConversationListener conversation;
        public FriendShipListener friend;
        public GroupListener group;
        public MessageListener message;
        public UserListener user;
        private Player()
        {
            conn = new ConnListener();
            conversation = new ConversationListener();
            friend = new FriendShipListener();
            group = new GroupListener();
            message = new MessageListener();
            user = new UserListener();
        }

        public static Platform Platform
        {
            get
            {
                if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
                {
                    return Platform.Windows;
                }
                else if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer)
                {
                    return Platform.MacOs;
                }
                else if (Application.platform == RuntimePlatform.Android)
                {
                    return Platform.Android;
                }
                else if (Application.platform == RuntimePlatform.IPhonePlayer)
                {
                    return Platform.IOs;
                }
                return Platform.Admin;
            }
        }

        public void Init(IMConfig config)
        {
            IMSDK.SetErrorHandler((errCode, errMsg) =>
            {
                Debug.LogError(errMsg);
            });

            IMSDK.SetConnListener(conn);
            IMSDK.SetConversationListener(conversation);
            IMSDK.SetFriendShipListener(friend);
            IMSDK.SetGroupListener(group);
            IMSDK.SetMessageListener(message);
            IMSDK.SetUserListener(user);
            IMSDK.InitSDK((suc) =>
            {
                Debug.Log("InitSDK:" + suc);
            }, config);
        }

        public void Login(string userId, string token)
        {
            IMSDK.Login((suc) =>
            {
                if (suc)
                {
                    OnLoginSuc(userId, token);
                }
                else
                {
                    Status = UserStatus.LoginFailed;
                    GameEntry.Event.Fire(this, new Event.OnLoginStatusChange()
                    {
                        UserStatus = UserStatus.LoginFailed
                    });
                }
            }, userId, token);
        }
        public void OnLoginSuc(string userId, string token)
        {
            this.UserId = userId;
            this.Token = token;
            Status = UserStatus.LoginSuc;
            GameEntry.Event.Fire(this, new Event.OnLoginStatusChange()
            {
                UserStatus = UserStatus.LoginSuc
            });
        }
    }
}
