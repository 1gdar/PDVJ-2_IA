using UnityEngine;
public abstract class SteeringBehaviour : MonoBehaviour
{
    protected Vector3 velocity = Vector3.zero;


    [SerializeField]
    protected Transform _target;
    [SerializeField]
    protected float _maxSpeed;
    protected bool _enabled = false;

    public abstract Vector3 Direction { get; }
    public Transform Target { get => _target; set => _target = value; }
}

