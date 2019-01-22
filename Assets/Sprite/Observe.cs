using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 观察者基类
/// </summary>
public abstract class Observe
{
    public abstract void OnNotify(Actor actor, ActorEvent actorEvent);
}



/// <summary>
/// 监听者基类
/// </summary>
public class Actor
{

}

/// <summary>
/// 观察者链表节点
/// </summary>
public class ObserveNode
{
    public ObserveNode next;
    public Observe observe { get; private set; }
    public ObserveNode(Observe observe )
    {
        this.observe = observe;
        next = null;
    }

}

/// <summary>
/// 被观察者基类
/// </summary>
public class subject
{
    protected List<ObserveNode> observes =new List<ObserveNode>();
    public subject()
    {
        observes = null;
    }

    public void AddObserve(ObserveNode node)
    {
        observes.Add(node);

    }

    public void RemoveObserve(ObserveNode node)
    {
        observes.Remove(node);
    }

    public void RemoveAllObserve()
    {
        observes = null;
    }

    protected void Notify(Actor actor,ActorEvent actorEvent)
    {
        foreach(var i in observes)
        {
            i.observe.OnNotify(actor, actorEvent);
        }
    }
    


}



