using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool _canHit = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamagable damagable = other.GetComponent<IDamagable>();
        if (damagable != null)
        {
            if (_canHit == true)
            {
                damagable.Damage();
                _canHit = false;
                StartCoroutine(AttackCoolDownRoutine());

            }
        }
    }

    IEnumerator AttackCoolDownRoutine()
    {
        yield return new WaitForSeconds(.5f);
        _canHit = true;
    }
}
