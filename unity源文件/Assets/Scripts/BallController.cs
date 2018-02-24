using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour {



    private Rigidbody ballrid;
    public Transform Aballtr;
    public Transform Noballtr;

    private AudioSource pong;
    public AudioSource shoot;
    public AudioSource winner;
    public AudioSource miss;

    private float f = 200;
    private int ready = 0;
    private int win = 0;
    private int onhand = 1;
    private int end = 0;
    private int time = 0;

    private int score1 = 0;
    private int score2 = 0;
 
    private Vector3 shootdir;
    private float sx = 0;
    private float sy = 0;
    private float sz = 0;

    public Text text1;
    public Text text2;
    public Text text3;

    // Use this for initialization
    void Start () {
        ballrid = GetComponent<Rigidbody>();
        ballrid.useGravity = false;
        pong = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update() {
        Shoot();//控制投篮
        PlayerController.Instance.Movement(onhand);
        if (end==1)
        {
            time++;
            if (time == 180)
            {
                Reset();
            }
        }
	}
    
    private void OnCollisionEnter(Collision collision)
    {
        pong.Play();
        string tag = collision.collider.tag;
        if (win == 0 && tag=="gg"&&end==0){
            miss.Play();
            text2.text = "Missing";
            end = 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;
        if (tag == "win") {
            winner.Play();
            string name = other.name;
            if (name == "Trigger1")
            {
                score1++;
            }
            else {
                score2++;
            }
            win = 1;
            end = 1;
            text2.text = "进球啦 !";
            text1.text = score1.ToString() + ":" + score2.ToString();
        }
    }


    void Shoot()
    {
        shootdir = Aballtr.rotation.eulerAngles;
        sx = Mathf.Cos((30 - shootdir.x) * Mathf.Deg2Rad) * Mathf.Sin(shootdir.y * Mathf.Deg2Rad);
        sy = Mathf.Sin((30 - shootdir.x)*Mathf.Deg2Rad);
        sz = Mathf.Cos((30 - shootdir.x) * Mathf.Deg2Rad) * Mathf.Cos(shootdir.y * Mathf.Deg2Rad);
        if (Input.GetMouseButton(0)&&onhand==1)
        {
            if (f < 320)
            {
                f += 0.4f;
            }
            ready = 1;
            text3.text = "蓄力中 " + f.ToString();
        }
        if (Input.GetMouseButtonUp(0) && ready == 1)
        {
            shoot.Play();
            ballrid.AddForce(new Vector3(sx, sy, sz) * f);
            ballrid.useGravity = true;
            text3.text = "";
            ready = 0;
            onhand = 0;
        }
    }

    private void Reset()
    {
        text2.text = " ";
        f = 200;
        ready = 0;
        win = 0;
        onhand = 1;
        end = 0;
        time = 0;
        
        ballrid.useGravity = false;
        ballrid.velocity = new Vector3(0, 0, 0);
        ballrid.transform.rotation = new Quaternion(1, 0, 0, 0);
        ballrid.transform.position = Noballtr.position;
        
        
    }

}
