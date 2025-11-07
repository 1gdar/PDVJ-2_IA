using UnityEngine;
public class SteeringBehaviour : MonoBehaviour
{
    protected Vector3 velocity = Vector3.zero;
    protected Transform _target;
    protected float _maxSpeed;

    public void SetAtributtes(Transform target , float maxSpeed)
    {
        _target = target;
        _maxSpeed = maxSpeed;
    }

    public virtual Vector3 SteeringDir
    {
        get;                  // cualquiera puede leer
        protected set;        // solo hijas o la base pueden escribir
    }

    public void ResetSteering()
    {
        SteeringDir = Vector3.zero;
    }




}

