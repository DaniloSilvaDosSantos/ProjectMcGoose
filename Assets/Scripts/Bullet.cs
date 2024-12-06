using Unity.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float initialShotForce = 2000;
    public int duration = 3;
    private float angle;
    private StartsHud startsHud;
    private Vector3 originalScale;
    private GameController gameController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * initialShotForce);

        angle = transform.rotation.z;

        originalScale = transform.localScale;

        startsHud = GameObject.Find("Stars").GetComponent<StartsHud>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
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
    }

    void ChangingAngleBullet(float velocityX, float velocityY)
    {
            float newAngle = Mathf.Atan2(velocityY, velocityX) * Mathf.Rad2Deg;
            
            if(newAngle != angle) duration --;
            angle = newAngle;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, newAngle));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Transform parent = other.transform.parent;

        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Destroy(other.gameObject);

        if (parent != null) Destroy(parent.gameObject);
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Object"))
        {
            Destroy(other.gameObject);
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Star"))
        {
            startsHud.starsAll[2] = true;
            Destroy(other.gameObject);
        }
    }
}
