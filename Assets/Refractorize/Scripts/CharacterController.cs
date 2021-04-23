using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool rotatingObject;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Vector2.zero.normalized);
        rb = gameObject.GetComponent<Rigidbody2D>();
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
                Debug.Log("hello");
                heldObject = hit.collider.gameObject;
                heldObject.transform.parent = gameObject.transform;
                heldObject.transform.position = Vector2.up;
            }
        }
    }

    private void FixedUpdate()
    {
        PhysRotateTowardMouse();
    }

    void PhysRotateTowardMouse()
    {
        rb.AddTorque(Vector2.Dot(-transform.right, (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized) * rotationSpeed);
        rb.AddForce(movementDirection);
    }
}
