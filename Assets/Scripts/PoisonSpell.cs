using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSpell : Spell
{
    [SerializeField] private float _poisonDuration = 4f;

    public float PoisonDuration => _poisonDuration;

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
        enemy.GetComponent<SpriteRenderer>().color = Color.green;
        yield return new WaitForSeconds(_poisonDuration);
        enemy.GetComponent<SpriteRenderer>().color = Color.white;
        Destroy(gameObject);
    }
}
