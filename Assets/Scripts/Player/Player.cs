using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{

    //get reference to Rigidbody
    private Rigidbody2D _rb2D;
    private PlayerAnimation _anim;
    [SerializeField] private float _speed = 5f;
    //jump force
    [SerializeField] private float _jumpForce = 5f;
    private bool _isGrounded = false;
    private bool _resetJumping = false;

    //diamonds
    [SerializeField] private int _diamonds;

    public int Health { get; set; }
    private int _maxHealth = 4;

    void Start()
    {
        //assign handle of rigidbody
        _rb2D = GetComponent<Rigidbody2D>();
        if (_rb2D == null)
            Debug.LogError("Rigidbody 2D is NULL!");

        _anim = GetComponent<PlayerAnimation>();
        if (_anim == null)
            Debug.LogError("Player Animation is NULL!");

        Health = 4;
        UIManager.Instance.UpdateDiamondAmount(_diamonds);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetMouseButtonDown(0) && IsGrounded())
        {
            _anim.AttackAnimation();
        }
    }

    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        _isGrounded = IsGrounded();
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            _rb2D.velocity = new Vector2(_rb2D.velocity.x, _jumpForce);
            _resetJumping = true;
            StartCoroutine(ResetJumpingRoutine());
            _anim.JumpAnimation(true);
        }


        _rb2D.velocity = new Vector2(horizontal * _speed, _rb2D.velocity.y);
        _anim.MoveAnimation(horizontal);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << 6);

        if (hit.collider != null)
        {
            if (_resetJumping == false)
            {
                _anim.JumpAnimation(false);
                return true;
            }
        }

        return false;

    }

    IEnumerator ResetJumpingRoutine()
    {
        yield return new WaitForSeconds(.1f);
        _resetJumping = false;
    }

    public void Damage()
    {
        Debug.Log("Player Got hit");
        _anim.Hit();
        Health--;
        //update UIDisplay
        UIManager.Instance.UpdateLives(Health);

        if (Health <= 0)
        {
            _anim.Death();
        }
    }

    public void AddDiamonds(int diamondAmount)
    {
        _diamonds += diamondAmount;
        UIManager.Instance.UpdateDiamondAmount(_diamonds);
    }

    public int CurrentDiamonds()
    {
        return _diamonds;
    }

    public void SubtractDiamonds(int diamondAmount)
    {
        _diamonds -= diamondAmount;
        UIManager.Instance.UpdateDiamondAmount(_diamonds);
    }
}
