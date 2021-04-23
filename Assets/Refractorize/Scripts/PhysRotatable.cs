using UnityEngine;

namespace JohnsLilHelper
{
    public class PhysRotatable
    {
        public static void PhysRotateTowardMouse(Rigidbody2D rb, float rotationSpeed)
        {
            rb.AddTorque(Vector2.Dot(-rb.transform.right, (Camera.main.ScreenToWorldPoint(Input.mousePosition) - rb.transform.position).normalized) * rotationSpeed);
        }
    }
}