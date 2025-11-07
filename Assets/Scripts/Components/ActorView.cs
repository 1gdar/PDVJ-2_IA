using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorView : MonoBehaviour
{
    private SystemHealth _systemHealth;

    private Animation _anim;

    private void Awake()
    {
        _anim = GetComponent<Animation>();
        _systemHealth = GetComponent<SystemHealth>();
    }
    private void OnEnable()
    {
        _systemHealth.OnTakeDamage += TakeDamageView;
        _systemHealth.OnDie += Die;
    }
    private void OnDisable()
    {
        _systemHealth.OnTakeDamage -= TakeDamageView;
        _systemHealth.OnDie -= Die;
    }

    public void TakeDamageView()
    {
        //Que ejecute una animacion
        _anim.Play();
    }

    public void Die()
    {
        gameObject.SetActive(false);  
    }

}
