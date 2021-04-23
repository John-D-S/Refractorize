using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JohnsLilHelper;

public class CharacterController : MonoBehaviour
{
    private Vector2 facingDirection;
    private Vector2 placementRotationDirection;
    private Vector2 movementDirection;
    [SerializeField]
    private float movementSpeed = 0.5f;
    [SerializeField]
    private float rotationSpeed = 1f;
    [SerializeField]
    private float maxObjectHeldDistance = 1;

    private GameObject heldObject;
    private Rigidbody2D heldObjectRB;
    private FixedJoint2D heldFixedJoint;
    private bool firstClick = true; // this is for detecting if this is the first or second click when picking up an object so that you don't immediately drop it
    private bool rotatingObject;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Vector2.zero.normalized);
        rb = gameObject.GetComponent<Rigidbody2D>();
        Physics2D.queriesStartInColliders = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 rawMovementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * movementSpeed;
        movementDirection = Vector2.Lerp(movementDirection, rawMovementDirection, 1);
        
        Debug.DrawRay(transform.position, gameObject.transform.up);
        
        if (Input.GetButtonDown("LeftClick") && !heldObject)
        {
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, gameObject.transform.up, maxObjectHeldDistance);
            

            // If it hits something...
            if (hit.collider.gameObject.GetComponent<Pickupable>())
            {
                heldObject = hit.collider.gameObject;
                heldFixedJoint = heldObject.GetComponent<FixedJoint2D>();
                heldFixedJoint.connectedBody = rb;
                heldFixedJoint.enabled = true;
                heldObject.transform.position = transform.TransformPoint(Vector2.up * 0.75f);
            }
        }
        else if (Input.GetButton("LeftClick") && heldObject)
        {
            PhysRotatable.PhysRotateTowardMouse(heldObjectRB, rotationSpeed);
        }
        else if (Input.GetButtonUp("LeftClick"))
        {
            heldFixedJoint.connectedBody = null;
            heldFixedJoint.enabled = false;
            heldObject = null;
        }
    }

    private void FixedUpdate()
    {
        PhysRotatable.PhysRotateTowardMouse(rb, rotationSpeed);
        rb.AddForce(movementDirection);
    }
}
