using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class Booter : MonoBehaviour
{
    private void Start()
    {
        //print(Time.time);
        //var controllers = Assembly.GetCallingAssembly().GetType();
        //var controllerType = typeof(IController);
        //List<IController> controllerList = new List<IController>();
        //var types = Assembly.GetCallingAssembly().GetTypes();
        //foreach (var i in types)
        //{
        //    Type[] tps = i.GetInterfaces();
        //    foreach (var tf in tps)
        //    {
        //        if (tf.FullName == controllerType.FullName)
        //        {
        //            IController con = Activator.CreateInstance(i) as IController;
        //            controllerList.Add(con);
        //        }
        //    }
        //}
        //foreach (var  item in controllerList)
        //{
        //    item.Init();
        //}
        //print(Time.time);

    }

    private void InitAllManager()
    {

    }
}
