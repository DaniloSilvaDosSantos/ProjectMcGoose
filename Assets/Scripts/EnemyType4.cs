using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyType4 : EnemyControler
{
    public int direction = 1;
    private Transform pointA;
    private float stopDistance = 0.2f;
    private float distance = 0;

    public Animator animator;
    public GroundChecker groundChecker;

    public override void Start()
    {
        base.Start();

        pointA = transform.parent.Find("PointA");
        animator = GetComponent<Animator>();
        groundChecker = GetComponentInChildren<GroundChecker>();
    }
    public override void EnemyIdleState()
    {
        base.EnemyIdleState();

        if(groundChecker.canDie)
        {
            currentEnemyState = EnemyState.Dying;
            animator.SetBool("isDead", true);
        } 

        currentIdleTime += Time.fixedDeltaTime;
        
        if(currentIdleTime >= idleTime)
        {
            currentIdleTime = 0f;
            Transform nextPointPosition;

            switch(currentIdlePoint)
            {
                case "a":
                    distance = 0;

                    if(direction >= 0)
                    {
                        nextPointPosition = transform.parent.Find("PointB");
                        nextPointDirection = (nextPointPosition.position - transform.position).normalized;
                    } 
                    else if (direction < 0){
                        nextPointPosition = transform.parent.Find("PointC");
                        nextPointDirection = (nextPointPosition.position - transform.position).normalized;
                    } 
                    
                    break;

                case "b":
                    nextPointPosition = transform.parent.Find("PointA");
                    nextPointDirection = (nextPointPosition.position - transform.position).normalized;
                    break;

                case "c":
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

        if(groundChecker.canDie)
        {
            currentEnemyState = EnemyState.Dying;
            animator.SetBool("isDead", true);
        } 

        rb.velocity = new UnityEngine.Vector2(nextPointDirection.x * walkSpeed, rb.velocity.y) ;

        if(distance == 0) return;

        distance = Vector2.Distance(transform.position, pointA.position);
        //Debug.Log(distance);

        if(distance < stopDistance)
        {
            transform.position = pointA.position;
            rb.velocity = new UnityEngine.Vector2(0,0);
            currentIdlePoint = "a";
            currentEnemyState = EnemyState.Idle;
        }

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
                direction = 1;
                currentEnemyState = EnemyState.Idle;
                return;
            }
            else if(other.gameObject.name == "PointC")
            {
                rb.velocity = new UnityEngine.Vector2(0,0);
                currentIdlePoint = "c";
                direction = -1;
                currentEnemyState = EnemyState.Idle;
                return;
            }
        }

        if(currentIdlePoint == "b")
        {
            if(other.gameObject.name == "PointB") return;
            else if(other.gameObject.name == "PointC") return;
            else if(other.gameObject.name == "PointA")
            {
                distance = Vector2.Distance(transform.position, pointA.position);
                direction = -1;

                return;
            }
        }

        if(currentIdlePoint == "c")
        {
            if(other.gameObject.name == "PointC") return;
            else if(other.gameObject.name == "PointB") return;
            else if(other.gameObject.name == "PointA")
            {
                distance = Vector2.Distance(transform.position, pointA.position);
                direction = 1;

                return;
            }
        }
    }
}
