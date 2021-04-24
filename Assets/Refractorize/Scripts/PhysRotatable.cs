using UnityEngine;

namespace JohnsLilHelper
{
    public class PhysRotatable
    {
        public static void PhysRotateTowardMouse(Rigidbody2D rb, float rotationSpeed)
        {
            rb.AddTorque(Vector2.Dot(-rb.transform.right, (Camera.main.ScreenToWorldPoint(Input.mousePosition) - rb.transform.position).normalized) * rotationSpeed);
        }

        public static void PhysRotateTowardMouse(Rigidbody2D rb, float rotationSpeed, Vector2 directionModifier)
        {
            rb.AddTorque(Vector2.Dot(-rb.transform.TransformVector(directionModifier), (Camera.main.ScreenToWorldPoint(Input.mousePosition) - rb.transform.position).normalized) * rotationSpeed);
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