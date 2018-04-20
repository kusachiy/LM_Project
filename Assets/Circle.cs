using System.Collections;
using System.Collections.Generic;
using Leap;
using Leap.Unity;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public GameObject[] Interactors;
    public GameObject Controller;
    public float Sensivity = 5f;
    private LeapServiceProvider _leap;
    private MeshRenderer _meshRenderer;
    public float Distance = 0.1f;
    private bool _handIsConnected = false;
    private Hand _hand;
    private Vector3 _previosHandRotation;
    private Vector2 _previosPos;
  
	// Use this for initialization
	void Start () {
        _leap = Controller.GetComponent<LeapServiceProvider>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _previosPos = Vector2.right;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void FixedUpdate()
    {
        CheckHands();
        if (_handIsConnected)
            RotateObject();
    }
    void CheckHands()
    {
        var frame = _leap.CurrentFrame;
        foreach (var hand in frame.Hands)
        {
            foreach (var finger in hand.Fingers)
            {
                if (Vector3.Distance(finger.TipPosition.ToVector3(), transform.position) < Distance)
                {
                    _meshRenderer.material.color = Color.red;
                    Debug.DrawLine(transform.position, finger.TipPosition.ToVector3(), Color.red);
                    _handIsConnected = true;
                    _hand = hand;
                    return;
                }
                _meshRenderer.material.color = Color.black;
                _handIsConnected = false;
            }
        }
    }
    void RotateObject()
    {
        if (float.IsNaN(_hand.PalmPosition.x)|| float.IsNaN(_hand.PalmPosition.y)|| float.IsNaN(_hand.PalmPosition.z))
        {
            Debug.Log("NAN SECURE OK");
            return;
        }
        var offset = _hand.PalmVelocity.ToVector3()* Sensivity;
        var position = new Vector2(_hand.PalmPosition.x - transform.position.x, _hand.PalmPosition.z - transform.position.z);
        var direction = Vector2.SignedAngle(position,_previosPos) >0?1:-1;//1 или -1
        var chord = Mathf.Sqrt(Mathf.Pow(offset.x, 2) + Mathf.Pow(offset.z, 2));
        var radius = Vector3.Distance(transform.position, _hand.PalmPosition.ToVector3());
        
        var rotateAngle = Mathf.Asin(Mathf.Deg2Rad * chord / (2 * radius))*2 * Mathf.Rad2Deg * direction;
        if (float.IsNaN(rotateAngle)) 
        {
            Debug.Log("Angle is NaN");
            return;
        }
        _previosPos = position;
        //Debug.Log(angle);

        transform.Rotate(0, rotateAngle, 0);
    }

}
