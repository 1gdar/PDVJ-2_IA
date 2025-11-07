
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AgentAIController : MonoBehaviour , IDestroyable
{
    private SystemHealth _systemHealth;
    private IDamager currentDamager;
    private float damageInterval = 0.5f;
    public float rotationSpeed;
    public SteeringBehaviour _currentSteering;
    private Rigidbody2D _rb;
    [SerializeField]
    private float maxSpeed = 4f;

    private void Awake()
    {
        _systemHealth = GetComponent<SystemHealth>();
        _rb= GetComponent<Rigidbody2D>();

    }

    private void Update()
    {

        // Dirección hacia la posición futura
        Vector2 direction = _currentSteering.Direction;

        // Mover hacia la posición futura
        transform.position += (Vector3)(direction * maxSpeed * Time.deltaTime);

        // Rotar para mirar hacia la dirección de movimiento
        if (direction.sqrMagnitude > 0.001f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }


    public void BehaviourMovement()
    {
        // Dirección deseada de velocidad
        Vector3 desiredVelocity = _currentSteering.Direction;

        // Diferencia (aceleración deseada)
        Vector3 acceleration = desiredVelocity - (Vector3)_rb.velocity;



        // Actualizar velocidad con aceleración limitada
        _rb.velocity += (Vector2)(acceleration * Time.deltaTime);

        // Limitar velocidad máxima
        if (_rb.velocity.magnitude > maxSpeed)
        {
            _rb.velocity = _rb.velocity.normalized * maxSpeed;
        }


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





    
