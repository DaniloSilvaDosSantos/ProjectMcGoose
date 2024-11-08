using System.Numerics;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class EnemyType2 : EnemyControler
{
    public override void EnemyIdleState()
    {
        base.EnemyIdleState();

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
            currentEnemyState = enemyState.Walk;
        }
    }

    public override void EnemyWalkState()
    {
        base.EnemyWalkState();

        rb.velocity = new UnityEngine.Vector2(nextPointDirection.x * walkSpeed, rb.velocity.y) ;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if(currentEnemyState != enemyState.Walk)
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
                currentEnemyState = enemyState.Idle;
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
                currentEnemyState = enemyState.Idle;
                return;
            }
        }

    }
}
