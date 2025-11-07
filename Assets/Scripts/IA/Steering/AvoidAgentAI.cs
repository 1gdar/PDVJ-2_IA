using UnityEngine;

public class AvoidAgentAI : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public float avoidDistance = 1f;
    public float rayDistance = 2f;         // Longitud de los rayos
    public float sideViewAngle = 45f;    // Ángulo lateral en grados
    public LayerMask obstacleLayer;

    [Header("Gizmos Colors")]
    public Color targetLineColor = Color.blue;
    public Color detectionRadiusColor = Color.red;
    public Color lateralDirsColor = Color.green;
    public Color activeDirColor = Color.yellow;

    private bool avoiding = false;
    private Vector2 avoidDir;
    private float rotationSpeed = 5f;

    void Update()
    {
        // Guardamos la posición actual del sprite como Vector2
        Vector3 position = transform.position;

        // Calculamos la dirección normalizada hacia el target (Seek)
        Vector3 seekDir = (target.position - position).normalized;

        // Inicialmente, la dirección de movimiento será hacia el target
        Vector3 moveDir = seekDir;

        /// Creamos
        /// El rayo frontal 
        /// Los laterales para deteccion de obstáculos

        // Rayo frontal: misma dirección que seekDir
        Vector2 forwardRay = seekDir;

        // Rayo derecho: rotamos seekDir hacia la derecha
        Vector2 rightRay = Quaternion.Euler(0, 0, sideViewAngle) * seekDir;

        // Rayo izquierdo: rotamos seekDir hacia la izquierda
        Vector2 leftRay = Quaternion.Euler(0, 0, -sideViewAngle) * seekDir;

        // Guardamos los 3 rayos en un array
        Vector2[] rayDirs = new Vector2[3] { forwardRay, rightRay, leftRay };

        // avoiding es false por defecto
        avoiding = false;

        foreach (var ray in rayDirs)
        {
            RaycastHit2D hit = Physics2D.Raycast(position, ray, rayDistance, obstacleLayer);
            if (hit.collider != null)
            {
                // Calculamos un vector que apunta desde el agente hacia el obstáculo
                Vector2 obstacle = (hit.collider.transform.position - position).normalized;

                // Calculamos una dirección perpendicular a obstacle para esquivar lateralmente
                Vector2 perp = new Vector2(-obstacle.y, obstacle.x);

                // Esta sería nuestra dirección de evasión 
                avoidDir = perp;

                // Aplicar dirección de movimiento
                moveDir = avoidDir;

                // Marcar que estamos esquivando
                avoiding = true;

                break; // Solo esquivar el primer obstáculo detectado
            }
        }

        // Mover el sprite
        transform.position += moveDir * speed * Time.deltaTime;

        if (moveDir.sqrMagnitude > 0.01f)
        {
            // Creamos la variable que apunta en la dirección de movimiento
            Vector3 dir = new Vector3(moveDir.x, moveDir.y, 0).normalized;

            // Creamos la rotación objetivo
            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.right, dir);

            // Rotación suave
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        }


    }

    void OnDrawGizmos()
    {
        Vector2 position = transform.position;

        // Línea hacia el target
        if (target != null)
        {
            Gizmos.color = targetLineColor;
            Gizmos.DrawLine(position, target.position);
        }

        // Radio de detección (lookahead)
        Gizmos.color = detectionRadiusColor;
        Gizmos.DrawWireSphere(position, rayDistance);

        // Dibujar los 3 rayos
        if (target != null)
        {
            Vector2 seekDir = ((Vector2)target.position - position).normalized;
            float rayOrientation = Mathf.Atan2(seekDir.y, seekDir.x);
            float rightRayOrientation = rayOrientation + sideViewAngle * Mathf.Deg2Rad;
            float leftRayOrientation = rayOrientation - sideViewAngle * Mathf.Deg2Rad;

            Vector2[] rayDirs = new Vector2[3];
            rayDirs[0] = seekDir * rayDistance;
            rayDirs[1] = new Vector2(Mathf.Cos(rightRayOrientation), Mathf.Sin(rightRayOrientation)) * rayDistance;
            rayDirs[2] = new Vector2(Mathf.Cos(leftRayOrientation), Mathf.Sin(leftRayOrientation)) * rayDistance;

            Gizmos.color = lateralDirsColor;
            foreach (var ray in rayDirs)
            {
                Gizmos.DrawLine(position, position + ray);
            }

            // Dirección activa de evasión
            if (avoiding)
            {
                Gizmos.color = activeDirColor;
                Gizmos.DrawLine(position, position + avoidDir);
            }
        }
    }
}
