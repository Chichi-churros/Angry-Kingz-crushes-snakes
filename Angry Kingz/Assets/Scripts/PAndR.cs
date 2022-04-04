using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAndR : MonoBehaviour
{
    Vector2 startPos;

    // The Force added upon releasepublic
    float force = 800;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        
    }

    void OnMouseDrag()
    {   // Convert mouse position to world position
        Vector2 p= Camera.main.ScreenToWorldPoint(Input.mousePosition);    // Keep it in a certain radius
        float radius = 1.8f;    
        Vector2 dir = p - startPos;    
        if (dir.sqrMagnitude > radius)        
            dir = dir.normalized * radius;    // Set the Position
        transform.position = startPos + dir;
    }


    void OnMouseUp()
    {
        // Disable isKinematic
        GetComponent<Rigidbody2D>().isKinematic = false;    // Add the Force
        Vector2 dir = startPos - (Vector2)transform.position;
        GetComponent<Rigidbody2D>().AddForce(dir * force);  // Remove the Script (not the gameObject)
        Destroy(this);
    }

}
