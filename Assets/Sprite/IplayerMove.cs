using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IplayerMove 
{
    void InputMonitor();
    void StopMove();
    void Move();
    void Disable();
    MoveType GetMoveType();
}

public interface Ibreak
{
    /// <summary>
    /// 爆破效果
    /// </summary>
    /// <param name="distance">强度参考数</param>
    /// <param name="pos">爆破效果点</param>
    void explosion(float distance, Vector3 pos);
}
public interface IListener
{
      void OnNotify(Actor actor);
}

public class ActionListener : IListener
{
    public Action<Actor> action;

    public  void OnNotify(Actor actor)
    {
        if (action != null) action.Invoke(actor);
    }
}