using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDetection : MonoBehaviour
{
    private IDamager currentDamager;
    [SerializeField]
    private float damageInterval = 0.5f;
    private SystemHealth _systemHealth;

    private void Awake()
    {
        _systemHealth = GetComponent<SystemHealth>();
    }
    public IEnumerator ApplyDamageCoroutine()
    {
        while (true)
        {
            if (currentDamager != null)
                _systemHealth.TakeDamage(currentDamager.Damage);

            yield return new WaitForSeconds(damageInterval);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Damage"))
        //{
        //    print("Done Damage");
        //    currentDamager = collision.gameObject.GetComponent<IDamager>();
        //    StartCoroutine(ApplyDamageCoroutine());
        //}
    }

 
    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Damage"))
        //{
        //    StopAllCoroutines();
        //    currentDamager = null;
        //}
    }
}
