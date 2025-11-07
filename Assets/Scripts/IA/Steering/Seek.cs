using UnityEngine;
public class Seek : SteeringBehaviour 
{

    [SerializeField]
    private float _steeringPower;
    private Vector2 seekDir;

    public Vector3 CalculateSteering()
    {
        Vector2 originPos = transform.position;
        Vector2 targetPos = _target.position;

        seekDir = targetPos - originPos;

        Vector3 steer = seekDir.normalized * _maxSpeed;
        steer *= _steeringPower;

        return steer;
    }
    public override Vector3 Direction
    {
        get
        {
           return CalculateSteering();
        }
    }

    private void OnDrawGizmos()
    {
        if (_target == null)
            return;

        Vector3 origin = transform.position;
        Vector3 desiredDir  = _target.position - transform.position;

        // Asumimos que desiredDirection ya fue calculada en CalculateSeekDir()


        Gizmos.color = Color.blue;
        Gizmos.DrawLine(origin,origin + desiredDir);
    }


}