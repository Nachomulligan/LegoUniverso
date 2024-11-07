using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour, IDamageable, IDeathLogic
{
    public float speed = 5f;
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask playerLayer;
    private bool isChasing = false;
    private BoxCollider triggerBox;
    
    public HealthComponent healthComponent;

    [SerializeField] private int dmgSound;
    private AudioManager audioManager;

    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
        audioManager = GameManager.Instance.audioManager;

        triggerBox = GetComponentInChildren<BoxCollider>();
    }

    private void Update()
    {
        if (isChasing)
        {
            Move();
        }
    }

    public void SetTriggerSize(Vector3 size)
    {
        triggerBox.size = size;
    }
    
    private void PlayDMGSound()
    {
        if (dmgSound >= 0 && dmgSound < audioManager.soundEffects.Count)
        {
            audioManager.PlaySFX(dmgSound);
        }
    }
    
    public void TakeDamage(float damage)
    {
        healthComponent.TakeDamage(damage);
        PlayDMGSound();
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
