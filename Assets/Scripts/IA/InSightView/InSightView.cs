using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class InSightView : MonoBehaviour
{
    [Header("Configuración del cono")]
    public float viewRadius = 5f;
    [Range(0, 360)] public float viewAngle = 90f;

    [Header("Colores del Gizmo")]
    public Color edgeColor = Color.green;
    public Color withoutDetect = Color.white;
    public Color withDetect = Color.red;

    [Header("Capas")]
    public LayerMask targetMask;    // Qué puede ser detectado
    public LayerMask obstacleMask;  // Qué bloquea la visión

    [Header("Debug")]
    public List<Transform> visibleTargets = new List<Transform>();

    private Transform currentTarget;

    private readonly Collider2D[] _targetsBuffer = new Collider2D[20];
    private int _targetsFound;

    private void Update()
    {
        FindVisibleTargets();
    }

    private void FindVisibleTargets()
    {
        visibleTargets.Clear();

        // Usa la versión NonAlloc (sin generar array nuevo)
        _targetsFound = Physics2D.OverlapCircleNonAlloc(transform.position, viewRadius, _targetsBuffer, targetMask);

        for (int i = 0; i < _targetsFound; i++)
        {
            Collider2D targetCollider = _targetsBuffer[i];
            if (targetCollider == null) continue;

            currentTarget = targetCollider.transform;

            if (IsInSight())
                visibleTargets.Add(currentTarget);
        }


    }

    public bool IsInSight()
    {
        if (currentTarget == null) return false;
        return InRange() && InAngle() && InView();
    }

    public bool InRange()
    {
        if (currentTarget == null) return false;
        float distance = Vector2.Distance(transform.position, currentTarget.position);
        return distance <= viewRadius;
    }

    private bool InAngle()
    {
        Vector2 forward = transform.right; // forward 2D
        Vector2 dirToTarget = (currentTarget.position - transform.position).normalized;
        float angleToTarget = Vector2.Angle(forward, dirToTarget);
        return angleToTarget <= viewAngle / 2f;
    }

    private bool InView()
    {
        Vector2 origin = transform.position;
        Vector2 dirToTarget = (currentTarget.position - (Vector3)origin).normalized;
        float distance = Vector2.Distance(origin, currentTarget.position);

        // Si no hay obstáculo entre medio, el target está visible
        return !Physics2D.Raycast(origin, dirToTarget, distance, obstacleMask);
    }

    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position;
        Vector3 forward = transform.right.normalized;

        // Bordes del cono
        Gizmos.color = edgeColor;
        Gizmos.DrawRay(origin, Quaternion.Euler(0, 0, viewAngle / 2f) * forward * viewRadius);
        Gizmos.DrawRay(origin, Quaternion.Euler(0, 0, -viewAngle / 2f) * forward * viewRadius);

        // Radio del cono
        if (visibleTargets.Count > 0)
        {
            Gizmos.color = withDetect; // rojo
        }
        else
        {
            Gizmos.color = withoutDetect; // blanco
        }

        Gizmos.DrawSphere(origin, viewRadius);

        // Dibuja líneas hacia los targets detectados
        foreach (Transform target in visibleTargets)
        {
            if (target == null) continue;

            if(target!=null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(origin, target.position);
            }
            
            
        }

    }
}
