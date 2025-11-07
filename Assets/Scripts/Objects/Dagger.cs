using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Dagger : MonoBehaviour, IDamager
{
    [field: SerializeField]
    public float Damage { get ; set; }
}
