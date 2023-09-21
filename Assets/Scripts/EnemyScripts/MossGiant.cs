using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamagable
{
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = base._health;
    }

   public void Damage()
    {
        Health--;
        _anim.SetTrigger("Hit");
        _isHit = true;
        _anim.SetBool("InCombat", true);

        if (Health <= 0)
        {
            _isAlive = false;
            _anim.SetTrigger("Death");

            if(!_anim.GetCurrentAnimatorStateInfo(0).IsName("Death"))
                Destroy(gameObject);
        }
    }
}
