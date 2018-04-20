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
    public float Distance = 0.1f;
    public float FreeRotationDrag = 0.01f;
    public float HandDrag = 2f;

    private LeapServiceProvider _leap;
    //private MeshRenderer _meshRenderer;
    private Vector3 _previosHandRotation;
    private Rigidbody _rigidbody;

    // Use this for initialization
    void Start()
    {
        _leap = Controller.GetComponent<LeapServiceProvider>();
        //_meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        CheckHands();
    }
    void CheckHands()
    {
        var frame = _leap.CurrentFrame;
        _rigidbody.angularDrag = FreeRotationDrag; 
       // _meshRenderer.material.color = Color.black;
        foreach (var hand in frame.Hands)
        {
            foreach (var finger in hand.Fingers)
            {
                if (Vector3.Distance(finger.TipPosition.ToVector3(), transform.position) < Distance)
                {
                    Debug.DrawLine(transform.position, finger.TipPosition.ToVector3(), Color.red);
                    RotateObject(hand);
                    //_meshRenderer.material.color = Color.red;
                    _rigidbody.angularDrag += HandDrag;
                    break;
                }
            }
        }
    }
    void RotateObject(Hand hand)
    {
        var offset = hand.PalmVelocity.ToVector3() * Sensivity;
        var position = new Vector2(hand.PalmPosition.x - transform.position.x, hand.PalmPosition.z - transform.position.z);
        var prevPosition = new Vector2(position.x - hand.PalmVelocity.x, position.y - hand.PalmVelocity.z);
        var direction = Vector2.SignedAngle(position, prevPosition) > 0 ? 1 : -1;//1 или -1
        var chord = Mathf.Sqrt(Mathf.Pow(offset.x, 2) + Mathf.Pow(offset.z, 2));
        var radius = Vector3.Distance(transform.position, hand.PalmPosition.ToVector3());

        var rotateAngle = Mathf.Asin(Mathf.Deg2Rad * chord / (2 * radius)) * 2 * Mathf.Rad2Deg * direction;
        if (float.IsNaN(rotateAngle))
        {
            Debug.Log("Angle is NaN");
            return;
        }
        _rigidbody.AddTorque(Vector3.up * rotateAngle,ForceMode.Acceleration);
        //transform.Rotate(0, rotateAngle, 0);
    }
    void AddForce()
    {
    }

}
