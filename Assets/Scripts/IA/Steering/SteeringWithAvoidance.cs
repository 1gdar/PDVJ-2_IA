using UnityEngine;

public class SteeringWithAvoidance : MonoBehaviour
{
    [SerializeField] private float sideViewAngle = 45f;
    [SerializeField] private float rayDistance = 2f;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private SteeringBehaviour _baseSteering;
    private RaycastHit2D _hit; // guardamos el último hit válido


    public bool ObstacleDetect()
    {
        Vector2 rayForward = _baseSteering.Direction;
        Vector2 rightRay = Quaternion.Euler(0, 0, sideViewAngle) * rayForward;
        Vector2 leftRay = Quaternion.Euler(0, 0, -sideViewAngle) * rayForward;

        Vector2[] rayDirs = { rayForward, rightRay, leftRay };

        foreach (var ray in rayDirs)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, ray, rayDistance, obstacleMask);
            if (hit.collider != null)
            {
                _hit = hit;
                return true;
            }
        }

        _hit = default;
        return false;
    }

    private Vector2 CalculateAvoidance()
    {
        if (_hit.collider == null)
            return Vector2.zero;

        Vector2 originPos = transform.position;
        Vector2 targetPos = _hit.collider.transform.position;

        // Vector hacia el obstáculo
        Vector2 obstacleDir = (targetPos - originPos).normalized;

        // Dirección perpendicular (ángulo 90°)
        Vector2 avoidDir = new Vector2(-obstacleDir.y, obstacleDir.x);

        return avoidDir;
    }


    public Vector3 GetDir
    {
        get
        {
            if (_baseSteering == null)
                return Vector3.zero;

            // Primero detectamos
            bool obstacleDetected = ObstacleDetect();

            // Si hay obstáculo, devolvemos dirección de evasión
            if (obstacleDetected)
                return CalculateAvoidance();

            // Si no hay obstáculo, seguimos la dirección base
            return _baseSteering.Direction;
        }
    }

    private void OnDrawGizmos()
    {
        if (_baseSteering == null) return;

        Vector2 origin = transform.position;
        Vector2 rayForward = _baseSteering.Direction;
        Vector2 rightRay = Quaternion.Euler(0, 0, sideViewAngle) * rayForward;
        Vector2 leftRay = Quaternion.Euler(0, 0, -sideViewAngle) * rayForward;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(origin, origin + rayForward * rayDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(origin, origin + rightRay * rayDistance);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(origin, origin + leftRay * rayDistance);
    }
}
