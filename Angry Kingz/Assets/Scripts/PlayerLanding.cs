using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLanding : MonoBehaviour
{
    public GameObject slingshot;
    public GameObject player;
    public Rigidbody2D rb;
    public int x = 10;
    public int y = 10;
    public Vector2 landingPosition;

    public bool slingshotEmpty;

    public Collider2D capsule;


    void Start()
    {
        landingPosition = slingshot.transform.position;
        rb = player.GetComponent<Rigidbody2D>();
        slingshotEmpty = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && slingshotEmpty)
        {
            slingshotEmpty = false;
            rb.isKinematic = true;  // mode : kinematic
            player.transform.position = landingPosition;    // set position

            rb.constraints = RigidbodyConstraints2D.FreezePosition;     // Freeze position

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        slingshotEmpty = true;

    }

}
