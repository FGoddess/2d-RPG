using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpell : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _radius;

    [SerializeField] private float _damage = 10;

    public float Damage => _damage;

    private Vector2 _direction = Vector2.zero;

    private void FixedUpdate()
    {
        if (_direction != Vector2.zero)
        {
            transform.Translate(_direction.normalized * _speed * Time.deltaTime);
        }
    }

    public void Initialize(Vector3 enemy)
    {
        _direction = enemy;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
