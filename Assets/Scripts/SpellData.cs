using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Create Spell")]
public class SpellData : ScriptableObject
{
    [SerializeField] public PlayerMovement _player;

}
