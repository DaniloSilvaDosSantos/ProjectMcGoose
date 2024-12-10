using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private Rigidbody2D rbEnemy;
    public bool canDie = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        rbEnemy = GetComponentInParent<Rigidbody2D>();
        
        if(other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if(rbEnemy.velocity.y < -5)
            {
                //EnemyControler enemyScript = transform.parent.GetComponentInParent<EnemyControler>();
                //enemyScript.currentEnemyState = EnemyControler.EnemyState.Dying;
                canDie = true;
            }
            else
            {
                rbEnemy.velocity = new Vector2(rbEnemy.velocity.x, 0);
            }
        }
    }
}
