using UnityEngine;


public class AgenteAvoidance : MonoBehaviour
{
    public Transform target;
    public float maxSpeed = 5f;
    public float maxForce = 10f;
    public float avoidDistance = 2f;
    public float avoidForce = 5f;
    public LayerMask obstacleMask;

    private Vector2 velocity = Vector2.zero;

    void Update()
    {
        // Obtener radio del CircleCollider2D
        CircleCollider2D circle = GetComponent<CircleCollider2D>();
        float radius = 0.5f;
        if (circle != null)
            radius = circle.radius;

        // 1. Seek hacia el objetivo
        Vector2 desired = ((Vector2)target.position - (Vector2)transform.position).normalized * maxSpeed;

        // 2. Obstacle avoidance con raycast
        Vector2 avoid = Vector2.zero;
        Vector2 rayOrigin = (Vector2)transform.position + (Vector2)transform.up * radius;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, transform.up, avoidDistance + radius, obstacleMask);
        if (hit.collider != null)
        {
            Vector2 hitNormal = hit.normal;
            avoid = hitNormal * avoidForce;
        }

        // 3. Combinamos seek + avoidance
        Vector2 steering = desired + avoid - velocity;

        // 4. Limitamos fuerza
        if (steering.magnitude > maxForce)
            steering = steering.normalized * maxForce;

        // 5. Actualizamos velocidad y posición
        velocity += steering * Time.deltaTime;
        if (velocity.magnitude > maxSpeed)
            velocity = velocity.normalized * maxSpeed;

        transform.position += (Vector3)(velocity * Time.deltaTime);

        // 6. Rotamos hacia la dirección del movimiento
        if (velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void OnDrawGizmos()
    {
        // Obtener radio del CircleCollider2D
        CircleCollider2D circle = GetComponent<CircleCollider2D>();
        float radius = 0.5f;
        if (circle != null)
            radius = circle.radius;

        Vector2 rayOrigin = (Vector2)transform.position + (Vector2)transform.up * radius;

        // 1. Desired (seek) vector
        Vector2 desired = Vector2.zero;
        if (target != null)
        {
            desired = ((Vector2)target.position - (Vector2)transform.position).normalized * maxSpeed;
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + desired);
        }

        // 2. Avoidance vector
        Vector2 avoid = Vector2.zero;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, transform.up, avoidDistance + radius, obstacleMask);
        if (hit.collider != null)
        {
            avoid = hit.normal * avoidForce;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + avoid);
        }

        // 3. Steering vector
        Vector2 steering = desired + avoid - velocity;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + steering);

        // 4. Dibujar el raycast hacia adelante
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(rayOrigin, rayOrigin + (Vector2)transform.up * (avoidDistance + radius));
    }
}
