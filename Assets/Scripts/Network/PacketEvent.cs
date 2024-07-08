using SocketLib;
using System;
using System.Collections.Generic;

public class PacketEventController
{
    private static Lazy<PacketEventController> m_instance = new Lazy<PacketEventController>(() => new PacketEventController());
    public static PacketEventController Instance => m_instance.Value;

    private Dictionary<Int32, IPacketEvent> m_eventByID;

    private PacketEventController()
    {
        m_eventByID = new Dictionary<int, IPacketEvent>();
    }

    public void AssignEvent(Int32 pPacketType,IPacketEvent pEvent)
    {
        m_eventByID.Add(pPacketType, pEvent);
    }

    public void OnPacket(IPacket pPacket)
    {
        if (m_eventByID.TryGetValue(pPacket.PacketType,out var packetEvent))
        {
            packetEvent.OnPacket(pPacket);
        }
    }
}

public interface IPacketEvent
{
    void OnPacket(IPacket pPacket);
}

public class PacketEvent<T> : IPacketEvent where T : IPacket, new()
{
    private static Lazy<PacketEvent<T>> m_instance = new Lazy<PacketEvent<T>>(()=>new PacketEvent<T>());
    public static PacketEvent<T> Instance => m_instance.Value;

    private List<IPacketListener<T>> m_listeners;

    private PacketEvent()
    {
        m_listeners = new List<IPacketListener<T>>();
        PacketEventController.Instance.AssignEvent(new T().PacketType,this);
    }

    public void Assign(IPacketListener<T> pListener)
    {
        m_listeners.Add(pListener);
    }
    
    public void Release(IPacketListener<T> pListener)
    {
        m_listeners.Remove(pListener);
    }

    public void OnPacket(T pPacket)
    {
        foreach (var listener in m_listeners)
        {
            listener.OnPacket(pPacket);
        }
    }

    public void OnPacket(IPacket pPacket) => OnPacket((T)pPacket);
}

