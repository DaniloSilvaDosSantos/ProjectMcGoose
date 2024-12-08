using System.Diagnostics;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    public float walkSpeed;
    public float idleTime;
    [HideInInspector] public float currentIdleTime;
    public float dyingAnimationDuration;
    [HideInInspector] public float dyingAnimationCount;
    [HideInInspector] public string currentIdlePoint;
    [HideInInspector] public Vector2 nextPointDirection;
    [HideInInspector] public enum EnemyState
    {
        Idle,
        Walk,
        Dying
    }
    [HideInInspector] public EnemyState currentEnemyState;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Vector3 originalScale;

    public virtual void Start()
    {
        currentEnemyState = EnemyState.Idle;
        currentIdleTime = 0f;
        dyingAnimationCount = 0f;
        currentIdlePoint = "a";

        rb = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        switch (currentEnemyState)
        {
            case EnemyState.Idle:
                EnemyIdleState();
                break;
            case EnemyState.Walk:
                EnemyWalkState();
                break;
            case EnemyState.Dying:
                EnemyDiyngState();
                break;
        }
    }

    public virtual void EnemyIdleState()
    {
        //
    }

    public virtual void EnemyWalkState()
    {
        //
    }

    public virtual void EnemyDiyngState()
    {
        //
    }

    public virtual void DestroyHimself()
    {
        Transform parent = transform.parent;
        
        Destroy(parent.gameObject);
        Destroy(gameObject);
    }

    public virtual void StartDeathAnimation()
    {
        foreach (Collider2D collider in GetComponents<Collider2D>())
        {
            collider.enabled = false;
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;

    }
}
