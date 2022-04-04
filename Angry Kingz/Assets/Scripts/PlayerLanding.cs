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

    void Start()
    {
        landingPosition = slingshot.transform.position;
        rb = player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.transform.position = landingPosition;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }

}
