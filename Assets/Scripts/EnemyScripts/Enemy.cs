using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamagable
{

    [SerializeField]
    protected float _speed = 5f;
    [SerializeField]
    protected int _health = 5;
    [SerializeField]
    protected int _gemsToDrop;
    [SerializeField]
    protected Transform _pointA, _pointB, _currentTarget;

    protected Animator _anim;
    protected SpriteRenderer _sprite;

    public virtual void Init()
    {
        _anim = GetComponentInChildren<Animator>();
        if (_anim == null)
            Debug.LogError("Animator is Null on " + this.gameObject.name);
        _sprite = GetComponentInChildren<SpriteRenderer>();
        if (_sprite == null)
            Debug.LogError($"Sprite Renderer is Null on {this.gameObject.name}");

        _currentTarget = _pointB;
    }

    private void Start()
    {
        Init();
    }

    protected virtual void Attack()
    {

    }

    public void Damage()
    {
        Debug.Log($"My name is {this.gameObject.name}");
    }

    protected virtual void Update()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            return;

        Movement();
    }
   
    protected virtual void Movement()
    {
        if (_currentTarget == _pointA)
            _sprite.flipX = true;
        else if (_currentTarget == _pointB)
            _sprite.flipX = false;

        if (transform.position == _pointA.position)
        {
            _currentTarget = _pointB;
            _anim.SetTrigger("Idle");
        }
        else if (transform.position == _pointB.position)
        {
            _currentTarget = _pointA;
            _anim.SetTrigger("Idle");

        }


        transform.position = Vector2.MoveTowards(transform.position, _currentTarget.position, _speed * Time.deltaTime);
    }
}
