using System.Collections;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private float _speed;
    [SerializeField] private float _radius;

    [SerializeField] private float _attackRange;

    private Collider2D _target;

    private bool _isAttacking;

    private void FixedUpdate()
    {
        if (_target == null)
        {
            _target = Physics2D.OverlapCircle(transform.position, _radius, _layerMask);
        }
    }

    private void Update()
    {
        if (_target != null && !_isAttacking)
        {
            Vector2 closestPoint = _target.bounds.ClosestPoint(transform.position);
            Vector2 direction = closestPoint - (Vector2)transform.position;
            transform.Translate(direction.normalized * _speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, closestPoint) < _attackRange)
            {
                _isAttacking = true;
                _target.TryGetComponent(out PlayerStats player);
                StartCoroutine(AttackRoutine(player));
            }
        }

        if (_isAttacking && Vector2.Distance(transform.position, _target.bounds.ClosestPoint(transform.position)) > _attackRange)
        {
            _isAttacking = false;
        }

    }

    private IEnumerator AttackRoutine(PlayerStats player)
    {
        while (_isAttacking)
        {
            player.TakeDamage(10);
            yield return new WaitForSeconds(2f);
        }
    }
}
