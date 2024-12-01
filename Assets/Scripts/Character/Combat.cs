using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    private Character player;

    [SerializeField] private Transform combatPoint;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private LayerMask enemyLayer;

    private bool canAttack = true;

    private Collider[] enemiesInRange = new Collider[3];

    private void Start()
    {
        player = GetComponent<Character>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        canAttack = false;

        float attackRadius = player.interactionRadius;

        int enemiesHit = Physics.OverlapSphereNonAlloc(combatPoint.position, attackRadius, enemiesInRange,enemyLayer);

        for (int i = 0; i < enemiesHit; i++)
        {
            var damageable = enemiesInRange[i].GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);
            }
        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
