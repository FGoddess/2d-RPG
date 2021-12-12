using UnityEngine;

public class SpellsHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] _spells;

    [SerializeField] private Transform _player;

    public void UseSpell(int spellIndex)
    {
        if (spellIndex > -1 && spellIndex < _spells.Length)
        {
            Instantiate(_spells[spellIndex], _player.transform.position, Quaternion.identity);
        }
    }
}
