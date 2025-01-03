﻿using GameFramework.Network;
namespace Dawn{
    public abstract class PacketHandlerBase : IPacketHandler
    {

        public abstract int Id{
            get;
        }

        public abstract void Handle(object sender,Packet packet);
    }
}

