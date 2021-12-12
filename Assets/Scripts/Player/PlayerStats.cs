using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private int _health;
    [SerializeField] private int _maxHealth;

    private int _mana; 
    [SerializeField] private int _maxMana;

    [SerializeField] private Image _healthImage;
    [SerializeField] private Image _manaImage;

    private float _currentHealthFill;
    private float _currentManaFill;

    [SerializeField] private float _lerpSpeed;

    private void Start()
    {
        _health = _maxHealth;
        _mana = _maxMana;

        _currentHealthFill = _health / _maxHealth;
        _currentManaFill = _mana / _maxMana;
    }

    private void TryChangeStat(ref int stat, int value, ref float statFill, ref int maxStat)
    {
        stat -= value;
        if(stat < 0)
        {
            stat = 0;
        }

        if(stat > maxStat)
        {
            stat = maxStat;
        }

        statFill = (float)stat / maxStat;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryChangeStat(ref _health, 10, ref _currentHealthFill, ref _maxHealth);
            TryChangeStat(ref _mana, 20, ref _currentManaFill, ref _maxMana);
        }

        if (_currentHealthFill != _healthImage.fillAmount)
        {
            _healthImage.fillAmount = Mathf.Lerp(_healthImage.fillAmount, _currentHealthFill, _lerpSpeed * Time.deltaTime);
        }

        if (_currentManaFill != _manaImage.fillAmount)
        {
            _manaImage.fillAmount = Mathf.Lerp(_manaImage.fillAmount, _currentManaFill, _lerpSpeed * Time.deltaTime);
        }
    }
}
