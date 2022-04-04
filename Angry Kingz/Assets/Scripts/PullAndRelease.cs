using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullAndRelease : MonoBehaviour
{

    public GameObject bird;

    //Default position
    Vector2 startPos;


    // Start is called before the first frame update
    void Start()
    {
        startPos = bird.transform.position;
    }

    void OnMouseDrag()
    {   // Convert mouse position to world position
        Vector2 p= Camera.main.ScreenToWorldPoint(Input.mousePosition);    // Keep it in a certain radius
        float radius = 1.8f;    
        Vector2 dir = p - startPos;    
        if (dir.sqrMagnitude > radius)        
            dir = dir.normalized * radius;    // Set the Position
        bird.transform.position = startPos + dir;
    }

}
