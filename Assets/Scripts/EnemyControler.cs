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
    [HideInInspector] public enum enemyState
    {
        Idle,
        Walk,
        dying
    }
    [HideInInspector] public enemyState currentEnemyState;
    [HideInInspector] public Rigidbody2D rb;

    public virtual void Start()
    {
        currentEnemyState = enemyState.Idle;
        currentIdleTime = 0f;
        dyingAnimationCount = 0f;
        currentIdlePoint = "a";

        rb = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        switch (currentEnemyState)
        {
            case enemyState.Idle:
                EnemyIdleState();
                break;
            case enemyState.Walk:
                EnemyWalkState();
                break;
            case enemyState.dying:
                EnemyDiyngState();
                break;
        }
    }

    public virtual void EnemyIdleState()
    {
        //gonas
    }

    public virtual void EnemyWalkState()
    {
        //jonas
    }

    public virtual void EnemyDiyngState()
    {
        //tomas
    }
}
