using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator _animator;

    private readonly string _attackLayerName = "Attack";
    private int _attackLayerIndex;
    private bool _isAtacking;

    private Coroutine _attackRoutine;

    [SerializeField] private float _attackDelay;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _attackLayerIndex = _animator.GetLayerIndex(_attackLayerName);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && _attackRoutine == null)
        {
            _attackRoutine = StartCoroutine(Attack());
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
