using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _health;

    [SerializeField] private Image _healthBar;
    [SerializeField] private float _timeToChangeHealth = 1f;

    private Coroutine _effectCoroutine;
    private Coroutine _healthCoroutine;

    private void Start()
    {
        _health = _maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Spell spell))
        {
            if(_effectCoroutine != null)
            {
                StopCoroutine(_effectCoroutine);
            }

            _effectCoroutine = StartCoroutine(spell.ApplyEffect(this));
            spell.gameObject.SetActive(false);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int value)
    {
        if (_healthCoroutine == null)
        {
            _healthCoroutine = StartCoroutine(ChangeHealth(_health));
        }
        else
        {
            StopCoroutine(_healthCoroutine);
            _healthCoroutine = StartCoroutine(ChangeHealth(_healthBar.fillAmount * _maxHealth));
        }

        _health -= value;
    }

    private IEnumerator ChangeHealth(float oldValue)
    {
        float timer = 0f;

        while (timer < _timeToChangeHealth)
        {
            _healthBar.fillAmount = Mathf.Lerp(oldValue / _maxHealth, _health / (float)_maxHealth, timer / _timeToChangeHealth);
            timer += Time.deltaTime;

            yield return null;
        }

        if (_health <= 0)
        {
            Die();
        }
    }
}
