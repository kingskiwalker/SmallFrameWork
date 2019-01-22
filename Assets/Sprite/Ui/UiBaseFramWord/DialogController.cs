using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager:MonoBehaviour
{
    public static DialogManager _instance;
    public static DialogManager Instance
    {
        get
        {
            if (_instance == null)
            {
                DialogManager[] dialogs = GameObject.FindObjectsOfType<DialogManager>();
                if (dialogs.Length == 1)
                {
                    _instance = dialogs[0];
                }
                else
                {
                    _instance = dialogs[0];
                    for(int i = 1; i < dialogs.Length; i++)
                    {
                        Destroy(dialogs[i].gameObject);
                    }
                }
            }
            return _instance;
        }
    }
    public List<DialogBase> dialogs = new List<DialogBase>();
    private Stack<DialogBase> dialogSaver = new Stack<DialogBase>();



    public void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void CreatView(DialogType type)
    {
        foreach (var I in dialogs)
        {
            if (I.Dialog == type)
            {
                //TODO 生成窗口？
                DialogBase dialog= Instantiate(I);

                dialogSaver.Push(dialog);
                dialog.ShowView();
                break;
            }
        }
    }

    
    public void CreatView(DialogType type,DialogShowType showType)
    {
        switch (showType)
        {
            case DialogShowType.CLOSE_OTHER:
                dialogSaver.Peek().Close();
                break;
            case DialogShowType.PROPUP:
                break;
        }
    }

    public void CloseNowView()
    {
        if (dialogSaver.IsNotNull())
        {
            dialogSaver.Peek().DestroyView();
        }
    }



}


public static class DkTool
{
    public static bool IsNotNull<T>(this Stack<T> ts)
    {
        if (ts != null && ts.Count > 0) return true;
        return false;
    }


}

public enum DialogType
{
    LEVEL_COMPLETE,
    LEVEL_SELECT,
    
    
}
public enum DialogShowType
{
    CLOSE_OTHER,
    PROPUP,
}
