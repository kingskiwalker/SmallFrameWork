using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dk.Base
{
    public class BaseEventManager<T>where T:new()
    {
        protected static T _instance;
        public static T Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new T();
                }
                return _instance;
            }

        }

        protected Dictionary<string, List<IListener>> Listeners = new Dictionary<string, List<IListener>>();

        public virtual void Addlistener(string eventName,IListener listener)
        {
            if (Listeners.ContainsKey(eventName))
            {
                List<IListener> mListeners = new List<IListener>();
                if (Listeners.TryGetValue(eventName, out mListeners))
                {
                    mListeners.Add(listener);
                }
                else
                {
                    mListeners.Add(listener);
                    Listeners[eventName] = mListeners;
                }
            }
            else
            {
                List<IListener> mListeners = new List<IListener>();
                mListeners.Add(listener);
                Listeners.Add(eventName, mListeners);
            }
        }

        public virtual void RemoveListener(string eventName,IListener listener)
        {
            //不存在该Key直接返回
            if (!Listeners.ContainsKey(eventName)) return;
            List<IListener> mlisteners = new List<IListener>();
            if (Listeners.TryGetValue(eventName, out mlisteners))
            {
                mlisteners.Remove(listener);
            }
            else
            {
                return;
            }
        }

        public virtual void Notify(string eventName, Actor actor)
        {
            List<IListener> listeners = new List<IListener>();
            if (Listeners.TryGetValue(eventName, out listeners))
            {
                foreach (var i in listeners)
                {
                    i.OnNotify(actor);
                }
            }
            else
            {
                Debug.Log("事件列表为空");
            }
        }

        public virtual void NotifyEvent()
        {

        }
    }
}