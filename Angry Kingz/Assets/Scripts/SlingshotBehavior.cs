using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotBehavior : MonoBehaviour
{
    // Bird prefab that will be spawned
    public GameObject birdPrefab;

    // Is there a Bird in the Trigger Area?
    bool occupied = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (!occupied)
            spawnNext();
    }

    void spawnNext()
    {
        // Spawn a Bird at current position with default rotation
        Instantiate(birdPrefab, transform.position, Quaternion.identity);
        occupied = true;
    }

    void OnTriggerExit2D(Collider2D co)
    {
        // Bird left the Spawn
        occupied = false;
    }


}
