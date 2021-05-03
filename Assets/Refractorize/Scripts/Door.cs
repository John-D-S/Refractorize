using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Activatable
{
    [SerializeField] 
    private Transform LeftDoor;
    private Vector3 leftDoorClosedPosition;
    [SerializeField]
    private Vector3 leftDoorOpenedPosition;

    [SerializeField] 
    private Transform RightDoor;
    private Vector3 rightDoorClosedPosition;
    [SerializeField]
    private Vector3 rightDoorOpenedPosition;

    [SerializeField]
    private float openSpeed = 1;
    private bool open = false;
    private bool openedLastFrame;
    //openProgress is a float between 0 and 1 which represents how open the door is. 0 is completely closed, 1 is fully open.
    private float openProgress = 0;
    
    private void Start()
    {
        leftDoorClosedPosition = LeftDoor.localPosition;
        rightDoorClosedPosition = RightDoor.localPosition;
    }

    public override void Activate()
    {
        open = true;
    }

    public override void Deactivate()
    {
        open = false;
    }

    private void FixedUpdate()
    {

        if (open && openProgress < 1)
        {
            openProgress += Time.fixedDeltaTime * openSpeed;
            if (openProgress < 0)
            {
                openProgress = 0;
            }
        }
        else if (!open && openProgress > 0)
        {
            openProgress -= Time.fixedDeltaTime * openSpeed;
            if (openProgress < 0)
            {
                openProgress = 0;
            }
        }

        LeftDoor.localPosition = Vector3.Lerp(leftDoorClosedPosition, leftDoorOpenedPosition, openProgress);
        RightDoor.localPosition = Vector3.Lerp(rightDoorClosedPosition, rightDoorOpenedPosition, openProgress);
    }
}
