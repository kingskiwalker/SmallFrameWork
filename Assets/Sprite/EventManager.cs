using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using Dk.Base;


/// <summary>
/// 观察事件
/// </summary>
public enum ActorEvent
{
    PLAYER_HIT,
    CUBE_BREAK

}
public class EventManager :BaseEventManager<EventManager>
{
    public override void Addlistener(string eventName, IListener listener)
    {
        base.Addlistener(eventName, listener);
    }

    public override void Notify(string eventName, Actor actor)
    {
        base.Notify(eventName, actor);
    }

    public override void RemoveListener(string eventName, IListener listener)
    {
        base.RemoveListener(eventName, listener);
    }

}

