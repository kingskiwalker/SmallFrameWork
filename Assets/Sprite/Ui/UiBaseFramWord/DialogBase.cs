using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class DialogBase : MonoBehaviour
{
    public DialogType Dialog;
    public static Action doAfterClose;
    public static Action doBefroClose;
    public Animator animator;
    protected const string Show = "SHOW";
    protected const string HIDE = "HIDE";

    private void Start()
    {
        OnStart();
    }

    public virtual void OnStart()
    {
        animator = GetComponent<Animator>();
    }


    public virtual void ShowView()
    {
        if (animator == null) animator = GetComponent<Animator>();
        animator.Play(Show);
    }

    public virtual void DestroyView()
    {        
        Destroy(this.gameObject);
        if (doAfterClose != null) doAfterClose.Invoke();
    }

    public virtual void Close(float progress=1)
    {
        if (doBefroClose != null) doBefroClose();
        animator.Play(HIDE);
        StartCoroutine(waitAnimEnd(HIDE, DoAfterCloseAnim, progress));
    }

    public virtual void DoAfterCloseAnim()
    {
        DialogManager.Instance.CloseNowView();
    }

    /// <summary>
    /// 等待动画控制器动画到达某个进度
    /// </summary>
    /// <param name="animName">动画名</param>
    /// <param name="action">结束后动作</param>
    /// <param name="progress">动画进度</param>
    /// <returns></returns>
    protected IEnumerator waitAnimEnd(string animName,Action action,float progress=1)
    {
        if (animator == null) yield break;
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        while (!info.IsName(animName))
        {
            info = animator.GetCurrentAnimatorStateInfo(0);
            yield return 1;
        }
        while (info.IsName(animName) && info.normalizedTime < progress)
        {
            info = animator.GetCurrentAnimatorStateInfo(0);
            yield return 1;
        }
        action.Invoke();

    }


   


}
