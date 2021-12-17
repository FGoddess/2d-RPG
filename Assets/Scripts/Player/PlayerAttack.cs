using System;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator _animator;

    private readonly string _attackLayerName = "Attack";
    private int _attackLayerIndex;

    private Coroutine _attackRoutine;

    [SerializeField] private float _attackDelay;
    [SerializeField] private GameObject _attackSpell;

    private bool _isCasting;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        SpellsHandler.SpellCasted += OnSpellCasting;
    }

    private void OnDisable()
    {
        SpellsHandler.SpellCasted -= OnSpellCasting;
    }

    private void OnSpellCasting(bool value)
    {
        _isCasting = value;
    }

    private void Start()
    {
        _attackLayerIndex = _animator.GetLayerIndex(_attackLayerName);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isCasting && _attackRoutine == null)
        {
            var ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (ray.collider != null && ray.collider.gameObject.TryGetComponent(out Enemy enemy))
            {
                _attackRoutine = StartCoroutine(Attack());

                Instantiate(_attackSpell, transform.position, Quaternion.identity);
            }
        }
    }

    private IEnumerator Attack()
    {
        _animator.SetLayerWeight(_attackLayerIndex, 1);
        _animator.SetBool("Attacking", true);
        yield return new WaitForSeconds(_attackDelay);
        _animator.SetBool("Attacking", false);
        _animator.SetLayerWeight(_attackLayerIndex, 0);

        _attackRoutine = null;
    }

}
