using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dk.Base;
public class UiEventManager :BaseEventManager<UiEventManager>
{


    private HashSet<IListener> oneceListener = new HashSet<IListener>();

    /// <summary>
    /// 只响应一次的Ui事件
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="actor"></param>
    public override void Notify(string eventName, Actor actor)
    {

        base.Notify(eventName, actor);
        List<IListener> removeList = new List<IListener>();
        foreach (var i in Listeners[eventName])
        {
            if (oneceListener.Contains(i))
            {
                removeList.Add(i);
            }
        }
        foreach(var i in removeList)
        {
            Listeners[eventName].Remove(i);
        }
    }

    public void Addlistener(string eventName, IListener listener,bool onece)
    {
        base.Addlistener(eventName, listener);
        if(onece) oneceListener.Add(listener);

    }
    public override void RemoveListener(string eventName, IListener listener)
    {
        base.RemoveListener(eventName, listener);
    }
}

public enum UiEventType
{
    LEVE_COMPLETE,
    NEXTlEVEL,
}
