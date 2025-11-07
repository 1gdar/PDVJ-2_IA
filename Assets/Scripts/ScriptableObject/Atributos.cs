using UnityEngine;

[CreateAssetMenu(fileName = "AtributosPJ", menuName = "Game/Personajes")]
public class Atributos : ScriptableObject
{
    public string _name;
    public int damage;
    public float _maxHealth;
    public float _levelChar;
    public Sprite _sprite;
    public Animator _animator;
    public GameObject _prefab;

}