using Unity.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float initialShotForce = 2000;
    public int duration = 3;
    private float angle;
    private StartsHud startsHud;
    private Timer timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * initialShotForce);

        angle = transform.rotation.z;

        startsHud = GameObject.Find("Stars").GetComponent<StartsHud>();
        timer = GameObject.Find("Timer").GetComponent<Timer>();
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
            if(CountingEnemies() > 0)
            {
                Debug.Log("Derrota");
            }
            else
            {
                Debug.Log("Vitoria");
                //startsHud.starsAll[0] = true;

                if(timer.executarFuncao)
                {
                    timer.StopTimer();
                    startsHud.starsAll[1] = true;
                }

                //chamar o GameManager                
            }

            Destroy(gameObject);
        }
    }

    void ChangingAngleBullet(float velocityX, float velocityY)
    {
            float newAngle = Mathf.Atan2(velocityY, velocityX) * Mathf.Rad2Deg;
            
            if(newAngle != angle) duration --;
            angle = newAngle;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, newAngle));
    }

    int CountingEnemies()
    {
        GameObject[] AllObjects = GameObject.FindObjectsOfType<GameObject>();
        int allEnemies = 0;

        for(int i = 0; i < AllObjects.Length; i++)
        {
            if(AllObjects[i].layer == LayerMask.NameToLayer("Enemy")) allEnemies++;
        }

        return allEnemies;
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
