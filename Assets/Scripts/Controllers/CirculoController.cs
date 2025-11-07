using System.Collections;
using UnityEngine;

public class CirculoController : MonoBehaviour
{
    private SystemHealth _sH;
    private SpriteRenderer _spr;

    [SerializeField]
    private Atributos _atributos;

   
    private void Awake()
    {
       
        _sH = GetComponent<SystemHealth>();
        _spr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
       _sH.SetHealth(_atributos._maxHealth);
        _spr.sprite = _atributos._sprite;
    }
    private void OnEnable()
    {
        _sH.OnDie += Die;
        _sH.OnTakeDamage += HitView;
    }
    private void OnDestroy()
    {
        _sH.OnDie -= Die;
        _sH.OnTakeDamage -= HitView;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Damage")) //tag o layer
        {
            IDamager _objectDamager = collision.GetComponent<IDamager>();  
            _sH.TakeDamage(_objectDamager.Damage);
            _spr.color = Color.red;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Damage"))
        {
            _spr.color = Color.white;
        }
    }

    public IEnumerator ChangeColor()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            _spr.color = Color.white;
        }
    }
    public void HitView()
    {
        _spr.color = Color.red;
        StartCoroutine(ChangeColor());
    }

    public void Die()
    {

        Destroy(gameObject);
    }
}
