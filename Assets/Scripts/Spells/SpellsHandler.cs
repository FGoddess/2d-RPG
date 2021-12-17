using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SpellsHandler : MonoBehaviour
{
    [SerializeField] private Spell[] _spells;

    [SerializeField] private Image _spellBar;
    [SerializeField] private Image _progressBar;

    private TextMeshProUGUI _castProgress;

    [SerializeField] private float _castTime;

    [SerializeField] private PlayerMovement _playerMover;
    public static event Action<bool> SpellCasted;

    private Coroutine _coroutine;


    private void Start()
    {
        _castProgress = _progressBar.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UseSpell(int spellIndex)
    {
        if (spellIndex > -1 && spellIndex < _spells.Length && _coroutine == null)
        {
            _progressBar.color = _spells[spellIndex].Color;
            _spellBar.gameObject.SetActive(true);

            _coroutine = StartCoroutine(StartCasting(_spells[spellIndex]));
        }
    }

    private IEnumerator StartCasting(Spell spell)
    {
        SpellCasted?.Invoke(true);

        float timer = 0f;

        while(timer < _castTime)
        {
            _progressBar.fillAmount = Mathf.Lerp(0, 1, timer / _castTime);
            _castProgress.text = $"{Mathf.Round(_progressBar.fillAmount * 100)}";
            timer += Time.deltaTime;

            yield return null;
        }

        _spellBar.gameObject.SetActive(false);
        spell.IsLeft = _playerMover.IsFliped;
        Instantiate(spell, _playerMover.transform.position, Quaternion.identity);
        SpellCasted?.Invoke(false);
        _coroutine = null;
    }
}
