using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _health;

    [SerializeField] private Image _healthBar;
    [SerializeField] private float _timeToChangeHealth = 1f;

    private Coroutine _coroutine;
    private Coroutine _healthCoroutine;

    private float _lerpValue;

    private void Start()
    {
        _health = _maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Spell spell))
        {
            if(_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(spell.ApplyEffect(this));
            spell.gameObject.SetActive(false);
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
    }
}
