using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Spell spell))
        {
            _health -= spell.Damage;
            Destroy(spell.gameObject);

            if (_health <= 0)
            {
                Die();
            }
        }

        if (collision.gameObject.TryGetComponent(out IceSpell iceSpell))
        {
            _health -= iceSpell.Damage;
            Destroy(iceSpell.gameObject);

            if (_health <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
