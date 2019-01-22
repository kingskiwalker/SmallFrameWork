using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObsBuilder : MonoBehaviour
{
    // Start is called before the first frame update
    public float radius = 20;
    public GameObject prefabs;
    private ActionListener creatCubeListener = new ActionListener();

    private void Start()
    {
        creatCubeListener.action += CreatSimpleCube;
        EventManager.Instance.Addlistener(ActorEvent.CUBE_BREAK.ToString(),creatCubeListener);
    }

    public void CreatSimpleCube(Actor actor)
    {
        Vector2 vector = UnityEngine.Random.insideUnitCircle;
        Vector3 creatPoin = new Vector3((int)(vector.x * radius), 0, (int)(vector.y * radius));
        creatPoin.y = 0.5f;
        Instantiate(prefabs, creatPoin, Quaternion.identity).SetActive(true);
    }

    private void OnDestroy()
    {
        EventManager.Instance.RemoveListener(ActorEvent.CUBE_BREAK.ToString(),creatCubeListener);        
    }


}



