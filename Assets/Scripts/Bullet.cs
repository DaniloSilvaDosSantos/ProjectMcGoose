//using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float initialShotForce = 2000;
    public int duration = 3;
    private float angle;
    private StartsHud startsHud;
    private Vector3 originalScale;
    private GameController gameController;
    private AudioSource audioSource;
    [SerializeField] private AudioClip collectingStar;
    [SerializeField] private AudioClip[] ricochet;
    [SerializeField] private AudioClip[] pinguimScream;
    [SerializeField] private GameObject shadow;
    private GameObject shadowInstance;
    [SerializeField] private float shadowCooldownMax = 0.25f;
    private float shadowCooldown;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        Debug.Log(audioSource);

        rb.AddForce(transform.right * initialShotForce);

        angle = transform.rotation.z;

        originalScale = transform.localScale;

        startsHud = GameObject.Find("Stars").GetComponent<StartsHud>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        shadowCooldown = shadowCooldownMax;
        shadowInstance = Instantiate(shadow, transform.position, transform.rotation);
    }

    void Update()
    {
        Vector2 velocity = rb.velocity;

        if(velocity.magnitude > 0.1f)
        {
            ChangingAngleBullet(velocity.x, velocity.y);
        }

        if(duration <= -1)
        {
            gameController.StartCoroutine(gameController.WinTheLevel());

            audioSource.PlayOneShot(ricochet[Random.Range(0, ricochet.Length)]);

            Destroy(shadowInstance);
            Destroy(gameObject);
        }

        if (rb.velocity.x > 0)
        {
            transform.localScale = new UnityEngine.Vector3(originalScale.x, -originalScale.y, originalScale.z);
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = originalScale;
        }

        shadowCooldown -= Time.deltaTime;
        
        if(shadowCooldown <=0)
        {
            shadowCooldown = shadowCooldownMax;
            SyncPositionAndRotation();
        }
    }

    void ChangingAngleBullet(float velocityX, float velocityY)
    {
        float newAngle = Mathf.Atan2(velocityY, velocityX) * Mathf.Rad2Deg;
            
        if(newAngle != angle){
            duration --;
            audioSource.PlayOneShot(ricochet[Random.Range(0, ricochet.Length)]);
        } 
        angle = newAngle;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, newAngle));
    }

    void SyncPositionAndRotation()
    {
        shadowInstance.transform.position = transform.position;
        shadowInstance.transform.rotation = transform.rotation;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Object"))
        {
            audioSource.PlayOneShot(ricochet[Random.Range(0, ricochet.Length)]);
            Destroy(other.gameObject);
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Star"))
        {
            startsHud.starsAll[2] = true;
            audioSource.PlayOneShot(collectingStar);
            Destroy(other.gameObject);
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            audioSource.PlayOneShot(pinguimScream[Random.Range(0, pinguimScream.Length)]);
        }
    }
}
