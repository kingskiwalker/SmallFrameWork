using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///   Manager关于全局 ,contorller关于场景
///     所以 应该是通过Booter用反射去取得所有managaer统一初始化
/// </summary>

public class BaseController<T>: MonoBehaviour,IController where  T: MonoBehaviour 
{
    private static T _instance;
    public static T Instance()
    {
        return _instance;
    }

    public void Init()
    {
        _instance = this as T;
        DontDestroyOnLoad(this.gameObject);
    }


}
public interface IController
{
    void Init();
 
}
