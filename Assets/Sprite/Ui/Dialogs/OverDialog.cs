using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverDialog : DialogBase
{
    public Button btn;
    public void Start()
    {
        btn.onClick.AddListener(CloseView);
    }
    private void CloseView()
    {
        Close();
    }
    public override void DoAfterCloseAnim()
    {
        base.DoAfterCloseAnim();
        UiEventManager.Instance.Notify(UiEventType.NEXTlEVEL.ToString(), new Actor());
    }

}
