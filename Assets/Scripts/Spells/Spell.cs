using System.Collections;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] protected LayerMask _layerMask;

    [SerializeField] protected float _speed;
    [SerializeField] protected float _radius;
    [SerializeField] protected int _damage = 10;
    [SerializeField] protected Color _color;

    [SerializeField] protected bool _isLeft;

    public float Damage => _damage;
    public Color Color => _color;

    protected Vector2 _direction;

    public bool IsLeft { set => _isLeft = value; }

    protected virtual void Start()
    {
        var enemies = Physics2D.OverlapCircleAll(transform.position, _radius, _layerMask);

        if (enemies.Length == 1)
        {
            _direction = enemies[0].bounds.center - transform.position;
        }
        else if (enemies.Length > 1)
        {
            var minLenght = 1000f;
            var nearestEnemy = enemies[0];

            foreach(var enemy in enemies)
            {
                if(Vector3.Distance(transform.position, enemy.transform.position) < minLenght)
                {
                    nearestEnemy = enemy;
                    minLenght = Vector3.Distance(transform.position, enemy.transform.position);
                }
            }

            _direction = nearestEnemy.bounds.center - transform.position;
        }
        else
        {
            _direction = _isLeft ? Vector2.left : Vector2.right;
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
