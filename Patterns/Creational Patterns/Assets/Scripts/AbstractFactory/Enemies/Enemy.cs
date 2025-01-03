using UnityEngine;

namespace AbstractFactory.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] protected int _health;
        [SerializeField] protected int _damage;
    }
}