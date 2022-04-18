using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAndR : MonoBehaviour
{
    Vector2 startPos;

    // The Force added upon releasepublic
    float force = 600;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        
    }


    void OnMouseUp()
    {
        // Disable isKinematic
        GetComponent<Rigidbody2D>().isKinematic = false;    
        
        // Add the Force
        Vector2 dir = startPos - (Vector2)transform.position;
        GetComponent<Rigidbody2D>().AddForce(dir * force);  
        
        // Remove the Script (not the gameObject)
        Destroy(this);
    }

}
