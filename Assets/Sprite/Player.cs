using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    [SerializeField]
    float speed=0.5f;
    private int breakNum = 0;
    public IplayerMove moveController;
    private ActionListener breakNumListener;
    Tween timeTween;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveController = GetComponent<IplayerMove>();
        breakNumListener = new ActionListener();
        breakNumListener.action += AddBreakNum;
        EventManager.Instance.Addlistener(ActorEvent.CUBE_BREAK.ToString(), breakNumListener);

    }

    private void AddBreakNum(Actor actor)
    {
        breakNum += 1;
        print(breakNum);
        if (breakNum == 10)
        {
            moveController.Disable();
            DialogManager.Instance.CreatView(DialogType.LEVEL_COMPLETE);
            ActionListener action = new ActionListener();
            action.action += (Actor a) =>
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            };
            UiEventManager.Instance.Addlistener(UiEventType.NEXTlEVEL.ToString(),action,true);
            //UiEventManager.Instance.Notify(UiEventType.LEVE_COMPLETE.ToString(), new Actor());
        }
    }

    // Update is called once per frame
    void Update()
    {
        //InputHandle();          
        moveController.InputMonitor();
        TimeMonitor();
    }

    private int GetInt(float oldvalue,bool forward)
    {
        if (Mathf.Abs(oldvalue) % 1 > 0)
        {
            if (!forward)
            {
                return (int)((oldvalue / 1) + 1);
            }
            else
            {

                return (int)((oldvalue / 1) - 1);
            }
        }
        return (int)oldvalue;
    }

    public void OnTriggeEnter(Collider other)
    {
        if (other.gameObject.tag == "UnBreak")
        {
            moveController.StopMove();
            Vector3 backPoin = new Vector3();
            backPoin = transform.position;
            switch (moveController.GetMoveType())
            {
                case MoveType.None:
                    
                    break;
                case MoveType.Forward:
                    backPoin.z= other.transform.position.z - Vector3.forward.z;
                    break;
                case MoveType.Back:
                    backPoin.z= other.transform.position.z - Vector3.back.z;
                    break;
                case MoveType.Left:
                    backPoin.x= other.transform.position.x - Vector3.left.x;
                    break;
                case MoveType.Right:
                    backPoin.x= other.transform.position.x - Vector3.right.x;
                    break;
            }
            //transform.position = backPoin;
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "UnBreak")
        {
            moveController.StopMove();
            timeTween.Kill();
            Time.timeScale = 1;
            Vector3 backPoin = new Vector3();
            backPoin = transform.position;
            switch (moveController.GetMoveType())
            {
                case MoveType.None:

                    break;
                case MoveType.Forward:
                    backPoin.z = other.transform.position.z - Vector3.forward.z;
                    break;
                case MoveType.Back:
                    backPoin.z = other.transform.position.z - Vector3.back.z;
                    break;
                case MoveType.Left:
                    backPoin.x = other.transform.position.x - Vector3.left.x;
                    break;
                case MoveType.Right:
                    backPoin.x = other.transform.position.x - Vector3.right.x;
                    break;
            }
            transform.position = backPoin;
        }else if (other.gameObject.tag == "CanBreak")
        {
            //如果为可破坏物体 调用破坏上的破坏方法
            other.gameObject.GetComponent<Ibreak>().explosion((moveController as TouchMove).moveActor.distance, transform.position);
            //抛出玩家的撞击事件
            EventManager.Instance.Notify(ActorEvent.PLAYER_HIT.ToString(), (moveController as TouchMove).moveActor);

        }
    }

    public void TimeMonitor()
    {
        if (Input.GetMouseButtonDown(0))
        {
            timeTween = DOTween.To(() => Time.timeScale, (x) => Time.timeScale = x, 0.1f, 0.5f);
        }else if (Input.GetMouseButtonUp(0))
        {
            timeTween.Kill();
            Time.timeScale = 1;
        }
    }

    private void OnDestroy()
    {

        EventManager.Instance.RemoveListener(ActorEvent.CUBE_BREAK.ToString(), breakNumListener);
    }

}




