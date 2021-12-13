using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellsHandler : MonoBehaviour
{
    [SerializeField] private Spell[] _spells;

    [SerializeField] private Image _spellBar;
    [SerializeField] private Image _progressBar;

    private TextMeshProUGUI _castProgress;

    [SerializeField] private float _castTime;

    [SerializeField] private Transform _player;

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

            _coroutine = StartCoroutine(Delay(_spells[spellIndex]));
        }
    }

    private IEnumerator Delay(Spell spell)
    {
        float timer = 0f;

        while(timer < _castTime)
        {
            _progressBar.fillAmount = Mathf.Lerp(0, 1, timer / _castTime);
            _castProgress.text = $"{Mathf.Round(_progressBar.fillAmount * 100)}";
            timer += Time.deltaTime;

            yield return null;
        }

        _spellBar.gameObject.SetActive(false);
        Instantiate(spell, _player.transform.position, Quaternion.identity);
        _coroutine = null;
    }
}
