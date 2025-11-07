using UnityEngine;

public class Pursuit : SteeringBehaviour
{

    private Vector2 futurePosition;
    private float targetSpeed = 3f;

    [SerializeField]
    private float turningFactor = 0.3f;

    public override Vector3 Direction => CalculateSteering();

    private Vector3 CalculateSteering()
    {
        Vector2 originPos = transform.position;
        Vector2 targetPos = _target.position;
        Vector2 targetForward = _target.right.normalized;

        // Distancia al target
        float distance = Vector2.Distance(originPos, targetPos);

        // a mayor distancia, mayor tiempo de predicción; a menor distancia, menor tiempo.
        float predictionTime = distance * turningFactor;

        // Posición futura 
        futurePosition = targetPos + targetForward * targetSpeed * predictionTime;

        Vector2 pursueDir = (futurePosition - originPos).normalized;

        // Dirección hacia la posición futura
        return pursueDir;



    }

    private void OnDrawGizmos()
    {
        if (_target == null) return;

        Vector2 originPos2D = transform.position;
        Vector2 targetPos2D = _target.position;

        float distance = Vector2.Distance(originPos2D, targetPos2D);

        float predictionTime = distance * turningFactor;

        Vector2 predictedPos2D = targetPos2D + (Vector2)_target.right.normalized * targetSpeed * predictionTime;

        // Convertir Vector2 a Vector3 para gizmos (z = 0)
        Vector3 originPos = new Vector3(originPos2D.x, originPos2D.y, 0);
        Vector3 predictedPos = new Vector3(predictedPos2D.x, predictedPos2D.y, 0);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(predictedPos, 0.1f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(originPos, predictedPos);
    }

}