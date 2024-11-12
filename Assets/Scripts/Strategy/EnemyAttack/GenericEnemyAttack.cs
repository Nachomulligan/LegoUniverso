using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemyAttack : MonoBehaviour
{
    [SerializeField] private Transform combatPoint;
    [SerializeField] private float attackDamage = 5f;
    [SerializeField] private float attackCooldown = 3f;
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private LayerMask targetLayer;

    private bool canAttack = true;
    private Collider[] targetsInRange = new Collider[1];

    [SerializeField] private int attackSound;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GameManager.Instance.audioManager;
    }

    private void Update()
    {
        if (canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private void PlayAttackSound()
    {
        if (attackSound >= 0 && attackSound < audioManager.soundEffects.Count)
        {
            audioManager.PlaySFX(attackSound);
        }
    }

    private IEnumerator Attack()
    {
        canAttack = false;
        
        int targetsHit = Physics.OverlapSphereNonAlloc(combatPoint.position, attackRadius, targetsInRange, targetLayer);
        
        for (int i = 0; i < targetsHit; i++)
        {
            var damageable = targetsInRange[i].GetComponent<IDamageable>();
            
            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);
                PlayAttackSound();
            }
        }
        
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(combatPoint.position, attackRadius);
    }
}
