using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float initialShotForce = 2000;
    public int duration = 3;
    private float angle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * initialShotForce);

        angle = transform.rotation.z;
    }

    void Update()
    {
        Vector2 velocity = rb.velocity;

        if(velocity.magnitude > 0.1f)
        {

            float newAngle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            
            if(newAngle != angle) duration --;
            angle = newAngle;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, newAngle));
        }

        if(duration <= -1)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("TNT"))
        {
            Destroy(other.gameObject);
        }
    }
}
