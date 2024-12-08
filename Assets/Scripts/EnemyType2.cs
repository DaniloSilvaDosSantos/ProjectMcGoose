using System.Numerics;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class EnemyType2 : EnemyControler
{
    public Animator animator;

    public override void Start()
    {
        base.Start();

        originalScale = transform.localScale;
        animator = GetComponent<Animator>();
    }
    public override void EnemyIdleState()
    {
        base.EnemyIdleState();

        animator.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));

        currentIdleTime += Time.fixedDeltaTime;
        
        if(currentIdleTime >= idleTime)
        {
            currentIdleTime = 0f;
            Transform nextPointPosition;

            switch(currentIdlePoint)
            {
                case "a":
                    nextPointPosition = transform.parent.Find("PointB");
                    nextPointDirection = (nextPointPosition.position - transform.position).normalized;
                    break;

                case "b":
                    nextPointPosition = transform.parent.Find("PointA");
                    nextPointDirection = (nextPointPosition.position - transform.position).normalized;
                    break;
            }
            currentEnemyState = EnemyState.Walk;
        }
    }

    public override void EnemyWalkState()
    {
        base.EnemyWalkState();

        animator.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
        if(rb.velocity.x < 0)
        {
            transform.localScale = new UnityEngine.Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
        else if(rb.velocity.x > 0)
        {
            transform.localScale = originalScale;
        }

        rb.velocity = new UnityEngine.Vector2(nextPointDirection.x * walkSpeed, rb.velocity.y) ;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") || other.CompareTag("Explosion"))
        {
            currentEnemyState = EnemyState.Dying;
            animator.SetBool("isDead", true);
        }

        if(currentEnemyState != EnemyState.Walk)
        {
            return;
        } 

        if(other.transform.parent != transform.parent)
        {
            
            return; 
        }
        

        if(currentIdlePoint == "a")
        {
            if(other.gameObject.name == "PointA") return;
            else if(other.gameObject.name == "PointB")
            {
                rb.velocity = new UnityEngine.Vector2(0,0);
                currentIdlePoint = "b";
                currentEnemyState = EnemyState.Idle;
                return;
            }
        }

        if(currentIdlePoint == "b")
        {
            if(other.gameObject.name == "PointB") return;
            else if(other.gameObject.name == "PointA")
            {
                rb.velocity = new UnityEngine.Vector2(0,0);
                currentIdlePoint = "a";
                currentEnemyState = EnemyState.Idle;
                return;
            }
        }

    }
}
