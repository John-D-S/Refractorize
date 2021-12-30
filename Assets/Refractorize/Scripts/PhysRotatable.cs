using UnityEngine;

namespace JohnsLilHelper
{
    public class PhysRotatable
    {
        public static void PhysRotateTowardMouse(Rigidbody2D rb, float rotationSpeed)
        {
            Vector3 screenToWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mousePosInWorld = new Vector3(screenToWorldPoint.x, screenToWorldPoint.y, 0);
            rb.AddTorque(Vector2.Dot(-rb.transform.right, (mousePosInWorld - rb.transform.position).normalized) * rotationSpeed);
        }

        public static void PhysRotateTowardMouse(Rigidbody2D rb, float rotationSpeed, Vector2 directionModifier)
        {
            Vector3 screenToWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mousePosInWorld = new Vector3(screenToWorldPoint.x, screenToWorldPoint.y, 0);
            rb.AddTorque(Vector2.Dot(-rb.transform.TransformVector(directionModifier), (mousePosInWorld - rb.transform.position).normalized) * rotationSpeed);
        }


        public static void PhysRotateTowardVector(Rigidbody2D rb, float rotationSpeed, Vector2 direction)
        {
            rb.AddTorque(Vector2.Dot(-rb.transform.right, direction) * rotationSpeed);
        }
        
        public static void PhysRotateTowardVector(Rigidbody2D rb, float rotationSpeed, Vector2 direction, Vector2 directionModifier)
        {
            rb.AddTorque(Vector2.Dot(-rb.transform.TransformVector(directionModifier), direction) * rotationSpeed);
        }

    }
}