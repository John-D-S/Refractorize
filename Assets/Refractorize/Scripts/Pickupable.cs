using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint2D), typeof(Rigidbody2D), typeof(Collider2D))]
public class Pickupable : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<FixedJoint2D>().enabled = false;
    }
}
