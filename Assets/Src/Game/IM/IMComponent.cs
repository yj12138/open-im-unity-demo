using UnityEngine;
using UnityGameFramework.Runtime;
using System.IO;
using OpenIM.IMSDK;
using OpenIM.Proto;
namespace Dawn.Game
{
    public class IMComponent : GameFrameworkComponent
    {
        public string wsAddr = "ws://192.168.101.4:10001";
        public string apiAddr = "http://192.168.101.4:10002";
        public string dataDir = "./OpenIM";
        public string logDir = "./OpenIM/Logs";
        public string dbDir = "./OpenIM";
        public uint logLevel = 5;
        public string imAdminToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySUQiOiJpbUFkbWluIiwiUGxhdGZvcm1JRCI6MTAsImV4cCI6MTczNjgxNjM0OCwibmJmIjoxNzI5MDQwMDQ4LCJpYXQiOjE3MjkwNDAzNDh9.ZWbe_KqfRBQyiyN6kt-DQFoWpp2UJwdhgJMMJiYMeKA";
        void Start()
        {
            var config = new IMConfig()
            {
                Platform = Player.Platform,
                WsAddr = wsAddr,
                ApiAddr = apiAddr,
                LogLevel = LogLevel.LevelDebug,
                IsLogStandardOutput = true,
                DataDir = Path.Combine(Application.persistentDataPath, dataDir),
                LogFilePath = Path.Combine(Application.persistentDataPath, logDir),
                DbPath = Path.Combine(Application.persistentDataPath, dbDir),
            };
            Player.Instance.Init(config);
        }
        void Update()
        {
            IMSDK.Polling();
        }

        void OnApplicationQuit()
        {
        }
    }
}

