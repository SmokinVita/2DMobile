using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamagable
{
    [SerializeField] private GameObject _acidPrefab;
    [SerializeField] private GameObject _spawnPoint;


    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = base._health;
    }

    protected override void Movement()
    {

    }

    public void Damage()
    {
        if (!_isAlive)
            return;

        Health--;
        if (Health <= 0)
        {
            _anim.SetTrigger("Death");

            var diamond = Instantiate(_diamondPrefab, transform.position, Quaternion.identity);
            var setvalue = diamond.GetComponent<Diamond>();
            if (setvalue != null)
            {
                setvalue.DiamonValue(_gemsToDrop);
            }

            Destroy(gameObject, 3f);
        }
    }

    public void Attack()
    {
        Instantiate(_acidPrefab, _spawnPoint.transform);
    }
}
