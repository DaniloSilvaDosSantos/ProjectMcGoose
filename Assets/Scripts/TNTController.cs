using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TNTController : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject explosionPrefab;
    private GameObject explosion;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        explosion.transform.SetParent(transform);
        explosion.SetActive(false);
    }

    void OnDestroy()
    {
        ActivatingExplosion();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(rb.velocity.x >= 2 || rb.velocity.y >= 2)
        {
            Destroy(gameObject);
        }
    }

    
    void ActivatingExplosion()
    {
        explosion.SetActive(true);
        explosion.transform.SetParent(null);

        Animator animator;
        animator = explosion.GetComponent<Animator>();
        animator.enabled = true;

        CircleCollider2D col;
        col = explosion.GetComponent<CircleCollider2D>();
        col.enabled = true;
    }
}
