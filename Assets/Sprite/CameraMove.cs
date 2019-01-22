using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraMove : MonoBehaviour,IListener
{
    // Start is called before the first frame update
    [SerializeField]
    Transform player;
    Vector3 offset;
    [SerializeField]
    float speed=2f;
    void Start()
    {
        offset = transform.position - player.position;
        EventManager.Instance.Addlistener(ActorEvent.PLAYER_HIT.ToString(), this);
        
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + offset,speed*Time.deltaTime);
    }
    public void OnNotify(Actor actor)
    {
        if ((actor as MoveActor).distance > 5) ShakCamera();        
    }
    public void ShakCamera()
    {

        this.transform.GetChild(0).DOShakePosition(1, 0.5f, 10, 90, false, true);
    }
    public void ResetScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void OnDestroy()
    {
        EventManager.Instance.RemoveListener(ActorEvent.PLAYER_HIT.ToString(), this);
    }


}
