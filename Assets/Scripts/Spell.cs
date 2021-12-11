using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _speed;
    [SerializeField] private float _radius;

    [SerializeField] private float _damage = 10;

    public float Damage => _damage;

    private Vector2 _direction = Vector2.zero;

    private void Start()
    {
        var enemy = Physics2D.OverlapCircle(transform.position, _radius, _layerMask);

        if (enemy != null)
        {
            _direction = enemy.bounds.center - transform.position;
        }
        else
        {
            Debug.Log("0 enemies");
            //Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(_direction != Vector2.zero)
        {
            transform.Translate(_direction.normalized * _speed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
