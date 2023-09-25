using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamagable
{
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = base._health;
    }

    public void Damage()
    {
        if (!_isAlive)
            return;

        Health--;
        _anim.SetTrigger("Hit");
        _isHit = true;
        _anim.SetBool("InCombat", true);

        if (Health <= 0)
        {
            _isAlive = false;
            _anim.SetTrigger("Death");

            GameObject diamond = Instantiate(_diamondPrefab, transform.position, Quaternion.identity);
            Diamond setvalue = diamond.GetComponent<Diamond>();
            if(setvalue != null)
            {
                setvalue.DiamonValue(_gemsToDrop);
            }
            Destroy(gameObject, 3f);
        }
    }


}
