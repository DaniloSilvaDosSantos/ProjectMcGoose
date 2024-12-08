using UnityEngine;

public class EnemyType1 : EnemyControler
{
    public Animator animator;

    public override void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();
    }
    public override void EnemyIdleState()
    {
        //Esse aqui não faz nada por enquanto
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
