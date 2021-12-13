using System.Collections;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] protected LayerMask _layerMask;

    [SerializeField] protected float _speed;
    [SerializeField] protected float _radius;
    [SerializeField] protected int _damage = 10;
    [SerializeField] protected Color _color;

    public float Damage => _damage;
    public Color Color => _color;

    protected Vector2 _direction = Vector2.zero;

    protected virtual void Start()
    {
        var enemy = Physics2D.OverlapCircle(transform.position, _radius, _layerMask);

        if (enemy != null)
        {
            _direction = enemy.bounds.center - transform.position;
        }
    }

    protected virtual void FixedUpdate()
    {
        if(_direction != Vector2.zero)
        {
            transform.Translate(_direction.normalized * _speed * Time.deltaTime);
        }
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    public virtual IEnumerator ApplyEffect(Enemy enemy) 
    {
        enemy.TakeDamage(_damage);
        yield return null;
    }
}
