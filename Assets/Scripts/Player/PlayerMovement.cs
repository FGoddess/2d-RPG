using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private static Vector2 _direction;

    private Animator _animator;

    public bool IsFliped => GetComponent<SpriteRenderer>().flipX;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        var _moveX = Input.GetAxisRaw("Horizontal");
        var _moveY = Input.GetAxisRaw("Vertical");

        _direction = new Vector2(_moveX, _moveY);

        Animate(_moveX, _moveY);
        transform.Translate(_direction.normalized * Time.deltaTime * _speed);
    }

    private void Animate(float xValue, float yValue)
    {
        if (xValue != 0)
            _animator.SetFloat("Right", xValue);
        else
            _animator.SetFloat("Right", yValue);
    }
}
