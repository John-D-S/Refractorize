using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up);
        Debug.Log(hit.point);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
