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
        Health--;
    }

    public void Attack()
    {
        Instantiate(_acidPrefab, _spawnPoint.transform);
    }
}
