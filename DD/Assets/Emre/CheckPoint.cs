using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Transform StartPosition;

    // Start is called before the first frame update
     Vector2 startpos;
    Rigidbody2D rb;
    private void Start()
    {
        startpos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        startpos = GameObject.Find("CheckPoint1").transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            Die();

        }
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            StartPosition.position= startpos;

        }
    }

    void Die()
    {
        Respawn();
    }
    void Respawn()
    {
        transform.position = StartPosition.position;
        rb.velocity = new Vector2(0, 0);
    }
}