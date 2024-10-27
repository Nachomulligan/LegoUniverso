using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour, IDamageable, IDeathLogic
{
    public float speed = 5f;
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask playerLayer;
    private bool isChasing = false;

    public HealthComponent healthComponent;

    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
    }

    private void Update()
    {
        if (isChasing)
        {
            Move();
        }
    }

    public void TakeDamage(float damage)
    {
        healthComponent.TakeDamage(damage);
    }

    public  void Die()
    {
        EnemyFactory enemyFactory = FindObjectOfType<EnemyFactory>();
        if (enemyFactory != null)
        {
            enemyFactory.ReturnToPool(this);
        }
    }

    private void Move()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 50f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            isChasing = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            isChasing = false;
        }
    }
}
