using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakStone : MonoBehaviour,Ibreak {

    [SerializeField]
    ParticleSystem particle;
    private float Power=50;
    public float Count=10;
    //Rigidbody rigidbody;
    Transform[] transforms;
    Vector3 expPos = new Vector3();
	// Use this for initialization
	void Start () {
        
        transforms = this.GetComponentsInChildren<Transform>();

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<Collider>().enabled = false;
            expPos = (transform.position + collision.transform.position) / 2f;
            expPos.y = Random.Range(0.1f, 0.7f);


        }
        
    }

    public void explosion(float destance,Vector3 pos)
    {
        //关闭当前物体碰撞体
        gameObject.GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        //粒子的生成
        expPos = (transform.position + pos) / 2f;
        expPos.y = Random.Range(0.1f, 0.7f);
        ParticleSystem par= Instantiate(particle, expPos,Quaternion.identity);
        par.transform.LookAt(transform);
        ParticleSystem.MainModule mainsetting = par.main;
        mainsetting.startSpeed= new ParticleSystem.MinMaxCurve(0.1f * destance, 0.4f * destance);
        par.Play();
        Destroy(par, 5f);
        //定义爆炸效果
        float hitPower = Mathf.Clamp(Power * destance/2, -500, 500);
        foreach (var i in transforms)
        {
            i.SetParent(null);
            if (i.GetComponent<Rigidbody>())
            {
                i.gameObject.GetComponent<MeshRenderer>().enabled = true;
                i.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                i.gameObject.GetComponent<Rigidbody>().AddExplosionForce(hitPower, expPos, Count);
                Destroy(i.gameObject, 5);   
            }
        }
        //TODO
        //if (EventManager.Instance.CreatCube != null) EventManager.Instance.CreatCube.Invoke();
        EventManager.Instance.Notify(ActorEvent.CUBE_BREAK.ToString(), new Actor());
        Destroy(gameObject);
    }
}
