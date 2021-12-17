using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSpell : Spell
{
    [SerializeField] private float _poisonDuration = 6f;
    [SerializeField] private int _poisonTicks = 3;

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
        enemy.GetComponent<SpriteRenderer>().color = _color;

        var secondToWait = _poisonDuration / _poisonTicks;
        int counter = 0;

        while(counter < _poisonTicks)
        {
            yield return new WaitForSeconds(secondToWait);
            enemy.TakeDamage(_damage);
            counter++;
        }

        yield return new WaitForSeconds(0.5f);
        enemy.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
