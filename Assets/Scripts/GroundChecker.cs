using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private Rigidbody2D rbEnemy;
    void Start()
    {
        rbEnemy = GetComponentInParent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if(rbEnemy.velocity.y < -5)
            {
                Transform grandparent = transform.parent.parent;
                Destroy(grandparent.gameObject);
            }
            else
            {
                rbEnemy.velocity = new Vector2(rbEnemy.velocity.x, 0);
            }
        }
    }
}
