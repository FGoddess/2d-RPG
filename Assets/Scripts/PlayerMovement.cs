using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector2 _direction;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        var _moveX = Input.GetAxisRaw("Horizontal");
        var _moveY = Input.GetAxisRaw("Vertical");

        _direction = new Vector2(_moveX, _moveY);

        Animate(_moveX);
        transform.Translate(_direction * Time.deltaTime * _speed);
    }

    private void Animate(float value)
    {
        _animator.SetFloat("Right", value);
    }
}
