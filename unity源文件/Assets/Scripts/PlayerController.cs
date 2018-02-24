using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    private static PlayerController _instance;
    public static PlayerController Instance
    {
        get
        {
            return _instance;
        }
    }



    public Transform Aballtr;
 
    private Transform playertr;
    private float x = 0;
    private float z = 0;
    private float rx = 0;
    private float ry = 0;
    private float fx = 0;
    private float fz = 0;
    private Vector3 p = new Vector3(0, 0, 0);
    private Vector3 v = new Vector3(0, 0, 0);

    private void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
    void Start () {
        playertr = GetComponent<Transform>();
	}
 	
	// Update is called once per frame
	void Update () {
        Screen.lockCursor = true;
    }

    public void Movement(int onhand) {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        rx = Input.GetAxis("Mouse Y");
        ry = Input.GetAxis("Mouse X");
        fz = z * Mathf.Cos(v.y * Mathf.Deg2Rad) - x * Mathf.Sin(v.y * Mathf.Deg2Rad);
        fx = x * Mathf.Cos(v.y * Mathf.Deg2Rad) + z * Mathf.Sin(v.y * Mathf.Deg2Rad);

        p = playertr.position;
        if ((fz > 0 && p.z < 132.9) || (fz < 0 && p.z > -132.9))
        {
            p.z = p.z + fz;
        }
        if ((fx > 0 && p.x < 67.9) || (fx < 0 && p.x > -67.9))
        {
            p.x = p.x + fx;
        }
        playertr.position = p;


        if ((rx < 0 && v.x < 60) || (rx > 0 && v.x > -60))
        {
            v.x = v.x - rx * 10;
        }
        v = v + new Vector3(0, ry, 0) * 10;
        playertr.rotation = Quaternion.Euler(v);
        if (onhand == 1)
        {
            Aballtr.position = playertr.position;
            Aballtr.rotation = Quaternion.Euler(v);
        }
    }


}
