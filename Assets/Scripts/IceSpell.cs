using System.Collections;
using UnityEngine;

public class IceSpell : Spell
{
    [SerializeField] private float _freezeDuration = 4f;

    public float FreezeDuration => _freezeDuration;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    public override IEnumerator ApplyEffect(Enemy enemy)
    {
        enemy.GetComponent<SpriteRenderer>().color = Color.blue;
        yield return new WaitForSeconds(_freezeDuration);
        enemy.GetComponent<SpriteRenderer>().color = Color.white;
        Destroy(gameObject);
    }
}
