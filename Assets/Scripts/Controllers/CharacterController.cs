using UnityEngine;

public class CharacterController : MonoBehaviour , IDestroyable
{
    [SerializeField] private float rotationSpeed = 100f;

    [SerializeField]
    private float _speed;

    private SystemHealth _systemHealth;


    private void Awake()
    {
        _systemHealth = GetComponent<SystemHealth>();
    }
    private void Start()
    {
        
    }
    private void OnEnable()
    {
        _systemHealth.OnDie += Die;
    }
    private void OnDisable()
    {
        _systemHealth.OnDie -= Die;
    }
    private void Update()
    {
        FollowMouse();
    }

    public void FollowMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z; // Mantener la coordenada Z

        // Velocidad constante sin importar la distancia
        transform.position = Vector2.MoveTowards(transform.position, mousePosition, _speed * Time.deltaTime);

        // Rotación
        Vector2 direction = ((Vector2)mousePosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

   

    public void Die()
    {
        GameManager.Instance.GameOver();

    }


}
