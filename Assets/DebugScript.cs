using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DebugScript : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;
    public GameObject cam3;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawLine(cam1.transform.position, cam2.transform.position, Color.red);
        Debug.DrawLine(cam1.transform.position, cam3.transform.position, Color.red);
        Debug.DrawLine(cam2.transform.position, cam3.transform.position, Color.red);

    }
}
