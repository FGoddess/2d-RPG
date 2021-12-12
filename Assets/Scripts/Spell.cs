using System.Collections;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] protected LayerMask _layerMask;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _radius;

    [SerializeField] protected float _damage = 10;

    public float Damage => _damage;

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
        yield return null;
        gameObject.SetActive(false); 
    }
}
