using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{

    //private Animator _anim;
    //private SpriteRenderer _sprite;


    /*private void Start()
    {
        _currentTarget = _pointB;

        _anim = GetComponentInChildren<Animator>();
        if (_anim == null)
            Debug.LogError("Animator is NULL!");

        _sprite = GetComponentInChildren<SpriteRenderer>();
        if (_sprite == null)
            Debug.LogError("Sprite Renderer is NULL!");
    }*/


    /*private void Movement()
    {
        if (_currentTarget == _pointA)
            _sprite.flipX = true;
        else if (_currentTarget == _pointB)
            _sprite.flipX = false;

        if(transform.position == _pointA.position)
        {
            _currentTarget = _pointB;
            _anim.SetTrigger("Idle");
        }
        else if(transform.position == _pointB.position)
        {
            _currentTarget = _pointA;
            _anim.SetTrigger("Idle");
        }

        transform.position = Vector2.MoveTowards(transform.position, _currentTarget.position, _speed * Time.deltaTime);
    }*/
}