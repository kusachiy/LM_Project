using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsScript : MonoBehaviour {

    public GameObject Controller;
    public float Speed;

   
    // Use this for initialization
    void Start () {
        
    }


    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.W))
            Controller.transform.position += Vector3.forward * Speed;
        if (Input.GetKeyDown(KeyCode.S))
            Controller.transform.position += Vector3.back * Speed;
        if (Input.GetKeyDown(KeyCode.A))
            Controller.transform.position += Vector3.left * Speed;
        if (Input.GetKeyDown(KeyCode.D))
            Controller.transform.position += Vector3.right * Speed;
        if (Input.GetKeyDown(KeyCode.T))
            Controller.transform.position += Vector3.up * Speed;
        if (Input.GetKeyDown(KeyCode.G))
            Controller.transform.position += Vector3.down * Speed;

    }
    // Update is called once per frame
    void Update () {
		
	}
}
