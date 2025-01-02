using GameFramework.Event;
using OpenIM.Proto;

namespace Dawn.Game.Event
{
    public class OnRecvMsg : GameEventArgs
    {
        public static readonly int EventId = typeof(OnRecvMsg).GetHashCode();
        public override int Id
        {
            get
            {
                return EventId;
            }
        }
        public bool IsOffline;
        public IMMessage Msg;

        public override void Clear()
        {
            Msg = null;
            IsOffline = false;
        }
    }
}
