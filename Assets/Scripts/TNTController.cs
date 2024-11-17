using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TNTController : MonoBehaviour
{
    private Rigidbody2D rb;

    public GameObject explosion;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnDestroy()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(rb.velocity.x >= 1 || rb.velocity.y >= 1)
        {
            Destroy(gameObject);
        }
    }
}
