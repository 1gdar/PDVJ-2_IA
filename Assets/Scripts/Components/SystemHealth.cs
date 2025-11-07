using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class SystemHealth: MonoBehaviour
{
    [SerializeField]
    private float damageInterval = 0.5f;

    public float CurrentHealth { get; private set; }

    public bool IsDead => CurrentHealth > 0;

    [field: SerializeField]
    public float MaxHealth { get  ; set ; }

    public event Action OnDie;
    public event Action OnTakeDamage;

    public void SetHealth(float maxHealth) 
    {
        CurrentHealth = maxHealth;
    }

    private void Start()
    {
       CurrentHealth = MaxHealth;
    }
    public void TakeDamage(float damage)
    {
        OnTakeDamage?.Invoke();
        CurrentHealth -= damage;
        if (CurrentHealth < 0) OnDie?.Invoke();

    }
    public IEnumerator ApplyDamage(IDamager damager)
    {
        while (true)
        {
            TakeDamage(damager.Damage);
            yield return new WaitForSeconds(damageInterval);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Damage"))
        {
            print("Done Damage");
            IDamager damager = collision.gameObject.GetComponent<IDamager>();
            StartCoroutine(ApplyDamage(damager));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Damage"))
        {
            StopAllCoroutines();
      
        }
    }




}

