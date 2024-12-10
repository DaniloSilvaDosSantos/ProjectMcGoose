using UnityEngine;

public class EnemyType1 : EnemyControler
{
    public Animator animator;
    public GroundChecker groundChecker;

    public override void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();
        groundChecker = GetComponentInChildren<GroundChecker>();
    }
    public override void EnemyIdleState()
    {
        if(groundChecker.canDie)
        {
            currentEnemyState = EnemyState.Dying;
            animator.SetBool("isDead", true);
        } 
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") || other.CompareTag("Explosion"))
        {
            currentEnemyState = EnemyState.Dying;
            animator.SetBool("isDead", true);
        }
    }
}
