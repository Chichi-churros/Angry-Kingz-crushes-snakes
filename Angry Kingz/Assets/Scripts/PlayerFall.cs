using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : MonoBehaviour
{
    private Vector2 startPos;   

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
        {
            PlayerHealth playerHealth = transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(20);
            transform.position = startPos;
        }
    }
}
