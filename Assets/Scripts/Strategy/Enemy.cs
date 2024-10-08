using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth;
    public float speed = 5f;
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask playerLayer;
    private bool isChasing = false;

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (isChasing)
        {
            Move();
        }
    }

    public  void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public  void Die() 
    {
        Destroy(gameObject);
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
