using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour
{

    [SerializeField] private Spider _spider;

    public void Fire()
    {
        _spider.Attack();
    }
}
