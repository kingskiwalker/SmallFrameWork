using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


/// <summary>
/// 画面镜头45度角的操作
/// </summary>
public class TouchMove : MonoBehaviour,IplayerMove
{
    private Vector2 startPoin; //鼠标移动起始点
    private Vector2 endPoin;//鼠标移动结束点
    private Tween moveTween; //移动动画控制器
    public MoveActor moveActor = new MoveActor();
    public float moveTime;
    float sensitivity = 20;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame


    public  void InputMonitor()
    {
        if (!this.enabled) return;
        if (Input.GetMouseButtonDown(0))
        {
            startPoin = Input.mousePosition;


        }else if (Input.GetMouseButtonUp(0))
        {
            endPoin = Input.mousePosition;
            Move();
            moveActor.distance = Vector2.Distance(startPoin, endPoin) / sensitivity;
        }
    }

    public void StopMove()
    {
        moveTween.Kill();
        //moveType = MoveType.None;
    }

    public void Move()
    {
        StopMove();
        //求出移动方向的标量 ，用标量位于哪个区间判断玩家移动方向
        Vector2 vector = (endPoin - startPoin).normalized;
        float distance = Vector2.Distance(endPoin, startPoin)/sensitivity;
        Vector3 direction = new Vector3();
        if (vector.x > 0 && vector.y > 0)
        {
            //前
            moveActor.moveType = MoveType.Forward;
            direction = Vector3.forward;

        } else if (vector.x > 0 && vector.y < 0)
        {
            //右 
            moveActor.moveType = MoveType.Right;
            direction = Vector3.right;

        } else if (vector.x < 0 && vector.y > 0)
        {
            //左
            direction = Vector3.left;
            moveActor.moveType = MoveType.Left;

        } else if (vector.x < 0 && vector.y < 0)
        {
            //后
            moveActor.moveType = MoveType.Back;
            direction = Vector3.back;
          
        }


        moveTween = transform.DOMove(transform.position + direction * distance, moveTime);
        //rb.AddForce(Vector3.back * distance,ForceMode.VelocityChange);
    }

    private float  GetDistance()
    {
        return Vector2.Distance(startPoin, endPoin)/sensitivity;
    }

    


    MoveType IplayerMove.GetMoveType()
    {
        return moveActor.moveType;
    }

    public void Disable()
    {
        this.enabled = false;
    }
}
public enum MoveType
{
    None,
    Forward,
    Back,
    Left,
    Right
}
[System.Serializable]
public class MoveActor : Actor
{
    public float distance { get; set; }
    public MoveType moveType { get; set; }

}