//using System.Collections;
//using System.Collections.Generic;
using Unity.Mathematics;
//using Unity.VisualScripting;
using UnityEngine;

public class TNTController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;
    public GameObject explosionPrefab;
    private GameObject explosion;
    private AudioSource audioSource;
    [SerializeField] private AudioClip explosionSFX;
    private GameObject sceneTransitionManager;

    void Start()
    {
        sceneTransitionManager = GameObject.Find("SceneTransitionManager");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        explosion.transform.SetParent(transform);
        explosion.SetActive(false);
    }

    void OnDestroy()
    {
        GameObject sceneTransitionManager = GameObject.Find("SceneTransitionManager");

        if(sceneTransitionManager != null)
        {
            if(sceneTransitionManager.GetComponent<SceneTransitionManager>().isLoadingScene == false)
            {
                ActivatingExplosion();
            }
        }
    }

    void Update()
    {
        animator.SetFloat("VelocityY", rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) Destroy(gameObject);

        if(math.abs(rb.velocity.x) >= 2 || rb.velocity.y >= 2)
        {
            Destroy(gameObject);
        }
    }

    
    public void ActivatingExplosion()
    {
        explosion.SetActive(true);
        explosion.transform.SetParent(null);

        Animator animator;
        animator = explosion.GetComponent<Animator>();
        animator.enabled = true;

        CircleCollider2D col;
        col = explosion.GetComponent<CircleCollider2D>();
        col.enabled = true;

        ExplosionController explosionController;
        explosionController = explosion.GetComponent<ExplosionController>();
        explosionController.enabled = true;

        audioSource.PlayOneShot(explosionSFX);
    }
}
