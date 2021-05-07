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
    private HingeJoint2D heldHingeJoint;
    private bool rotatingHeldObject;
    private float originalHeldObjectDrag;
    private Vector2 heldObjectFacingDireciton;
    
    private Vector2 tempPlayerFaceDirection;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        //  Physics2D.queriesStartInColliders = false;
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

            if (hit)
            {
                if (hit.collider.gameObject.GetComponent<Pickupable>())
                {
                    heldObject = hit.collider.gameObject;
                    heldHingeJoint = heldObject.GetComponent<HingeJoint2D>();
                    heldObjectRB = heldObject.GetComponent<Rigidbody2D>();
                    heldHingeJoint.connectedBody = rb;
                    heldHingeJoint.enabled = true;
                    originalHeldObjectDrag = heldObjectRB.drag;
                    heldObjectRB.drag = 0;
                    heldObjectFacingDireciton = -heldObject.transform.InverseTransformVector(Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position).normalized;
                }

            }
            // If it hits something...
        }
        else if (Input.GetButtonDown("LeftClick") && heldObject)
        {
            rotatingHeldObject = true;
            tempPlayerFaceDirection = transform.up;
        }
        else if (Input.GetButtonUp("LeftClick") && rotatingHeldObject)//putting down the object
        {
            DropObject();
        }
    }

    public void DropObject()
    {
        if (heldObject || rotatingHeldObject)
        {
            heldObjectRB.drag = originalHeldObjectDrag;
            heldHingeJoint.connectedBody = null;
            heldHingeJoint.enabled = false;
            heldHingeJoint = null;
            heldObjectRB = null;
            heldObject = null;
            rotatingHeldObject = false;
        }
    }

    private void FixedUpdate()
    {
        //for rotating the object
        if (heldObject)
        {
            if (!rotatingHeldObject)// rotate held object toward facing direction
            {
                PhysRotatable.PhysRotateTowardVector(heldObjectRB, rotationSpeed, transform.right, heldObjectFacingDireciton.normalized);
            }
            else if (rotatingHeldObject)// rotate held object toward mouse and keep player's rotation from drifting;
            {
                PhysRotatable.PhysRotateTowardMouse(heldObjectRB, rotationSpeed, Vector2.Perpendicular(heldObjectFacingDireciton.normalized));
                PhysRotatable.PhysRotateTowardVector(rb, rotationSpeed, tempPlayerFaceDirection);
            }
        }

        //for rotating the player
        if (!rotatingHeldObject)
        {
            if (!heldObject)
            {
                PhysRotatable.PhysRotateTowardMouse(rb, rotationSpeed);
            }
            else if (heldObject)
            {
                PhysRotatable.PhysRotateTowardMouse(rb, rotationSpeed * 2);
            }
        }
        rb.AddForce(movementDirection);
    }
}
