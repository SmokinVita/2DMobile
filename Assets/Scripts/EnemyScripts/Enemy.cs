using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    [SerializeField]
    protected float _speed = 5f;
    [SerializeField]
    protected int _health = 5;
    [SerializeField]
    protected int _gemsToDrop;
    [SerializeField]
    protected GameObject _diamondPrefab;
    [SerializeField]
    protected Transform _pointA, _pointB, _currentTarget;

    protected Animator _anim;
    protected SpriteRenderer _sprite;
    protected bool _isHit;
    protected bool _isAlive = true;

    protected Player _player;

    public virtual void Init()
    {
        _anim = GetComponentInChildren<Animator>();
        if (_anim == null)
            Debug.LogError("Animator is Null on " + this.gameObject.name);
        _sprite = GetComponentInChildren<SpriteRenderer>();
        if (_sprite == null)
            Debug.LogError($"Sprite Renderer is Null on {this.gameObject.name}");

        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
            Debug.LogError("Player is NULL!");

        _currentTarget = _pointB;
    }

    private void Start()
    {
        Init();
    }

    protected virtual void Update()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            return;

        if (_isAlive == false)
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

        float distance = Vector2.Distance(transform.position, _player.transform.position);
        if (distance > 2f)
        {
            _isHit = false;
            _anim.SetBool("InCombat", false);
        }

        if (_isHit == true)
        {
            Vector2 direction = _player.transform.localPosition - transform.localPosition;
            if (direction.x < 0)
            {
                _sprite.flipX = true;
            }
            else if (direction.x > 0)
            {
                _sprite.flipX = false;
            }
        }

        if (_isHit == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, _currentTarget.position, _speed * Time.deltaTime);
        }
    }
}
