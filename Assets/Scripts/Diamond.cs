using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    //value from enemy
    [SerializeField] private int _value;

    public void DiamonValue(int value)
    {
        _value = value;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.AddDiamonds(_value);
                Destroy(this.gameObject);
            }
        }
    }
}
