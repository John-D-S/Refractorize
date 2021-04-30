using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LazerActivator : MonoBehaviour
{
    [SerializeField] private Activatable activatable;

    public void Activate()
    {
        activatable.PerformFunction();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Activator";   
    }
}
