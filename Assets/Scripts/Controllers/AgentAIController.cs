
using System.Collections;
using UnityEngine;

public class AgentAIController : MonoBehaviour , IDestroyable
{
    private SystemHealth _systemHealth;
    private IDamager currentDamager;
    private float damageInterval = 0.5f;
    public float rotationSpeed;


    private void Awake()
    {
        _systemHealth = GetComponent<SystemHealth>();
    }

    private void Update()
    {
        BehaviourMovement();
    }

    public void BehaviourMovement()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    private void OnEnable()
    {
        _systemHealth.OnDie += Die;
    }
    private void OnDisable()
    {
        _systemHealth.OnDie -= Die;
    }
    public void Die()
    {
        Destroy(gameObject);
    }


    public IEnumerator ApplyDamage()
    {
        while (true)
        {
            _systemHealth.TakeDamage(currentDamager.Damage);
            yield return new WaitForSeconds(damageInterval);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Damage"))
        {
            print("Done Damage");
            currentDamager = collision.gameObject.GetComponent<IDamager>();
            StartCoroutine(ApplyDamage());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Damage"))
        {
            StopAllCoroutines(); 
            currentDamager = null;
        }
    }



}





    
