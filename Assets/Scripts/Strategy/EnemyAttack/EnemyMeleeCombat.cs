using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeCombat : MonoBehaviour
{
    [SerializeField] private Transform combatPoint;
    [SerializeField] private float attackDamage = 5f;
    [SerializeField] private float attackCooldown = 3f;
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private LayerMask playerLayer;

    private Enemy enemy;

    private float timeSinceLastAttack = 0f;

    private Collider[] playerInRange = new Collider[1];

    [SerializeField] private int attackSound;
    private AudioManager audioManager;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        audioManager = GameManager.Instance.audioManager;
    }
    
    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        
        if (timeSinceLastAttack >= attackCooldown)
        {
            Attack();
        }

        CheckAttackRange();
    }

    private void CheckAttackRange()
    {
        if (IsPlayerInRange())
        {
            enemy.speed = 0f;
        }
        else
        {
            enemy.speed = 5f;
        }
    }

    private void PlayAttackSound()
    {
        if (attackSound >= 0 && attackSound < audioManager.soundEffects.Count)
        {
            audioManager.PlaySFX(attackSound);
        }
    }

    private bool IsPlayerInRange()
    {
        return Physics.OverlapSphereNonAlloc(combatPoint.position, attackRadius, playerInRange, playerLayer) > 0;
    }

    private void Attack()
    {
        int playerHit = Physics.OverlapSphereNonAlloc(combatPoint.position, attackRadius, playerInRange, playerLayer);

        for (int i = 0; i < playerHit; i++)
        {
            var damageable = playerInRange[i].GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);
                PlayAttackSound();
            }
        }
        
        timeSinceLastAttack = 0f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(combatPoint.position, attackRadius);
    }
}
