using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private float _speed;
    [SerializeField] private float _radius;

    protected Vector2 _direction;

    private Collider2D _target;

    private bool _isAttacking;

    private void Start()
    {
        _target = Physics2D.OverlapCircle(transform.position, _radius, _layerMask);
    }

    // Update is called once per frame
    private void Update()
    {
        if(_target != null && !_isAttacking)
        {
            Vector2 direction = _target.bounds.ClosestPoint(transform.position) - transform.position;
            transform.Translate(direction.normalized * _speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerStats player))
        {
            Debug.Log("da");
            _isAttacking = true;
            StartCoroutine(AttackRoutine(player));
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
