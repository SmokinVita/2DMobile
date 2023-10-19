using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityStandardAssets.CrossPlatformInput;

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
    private Vector2 _respawnLocation;

    public int Health { get; set; }
    [SerializeField] private int _maxHealth = 4;
    private bool _isAlive = true;

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

        _respawnLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAlive == false)
            return;

        Movement();

        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            Heal();
        }

        if (CrossPlatformInputManager.GetButtonDown("A_Button") && IsGrounded())
        {
            _anim.AttackAnimation();
        }
    }

    private void Movement()
    {
        float horizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        _isGrounded = IsGrounded();

        if ((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("B_Button")) && IsGrounded() == true)
        {
            if(GameManager.Instance._hasBootsOfFlight)
                _rb2D.velocity = new Vector2(_rb2D.velocity.x, _jumpForce + 2);
            else
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
        if(_isAlive == false)
            return;

        Debug.Log("Player Got hit");
        _anim.Hit();
        Health--;
        //update UIDisplay
        UIManager.Instance.UpdateLives(Health);

        if (Health <= 0)
        {
            _isAlive = false;
            _anim.Death();
        }
    }

    public void AddDiamonds(int diamondAmount)
    {
        _diamonds += diamondAmount;
        Debug.Log("God Diamonds");
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

    public void Respawn()
    {
        _anim.Fell();
        transform.position = _respawnLocation;
    }

    public void SetRespawnLocation(Vector2 newPos)
    {
        _respawnLocation = newPos;
    }

    public void Heal()
    {
        Debug.Log("Heal was called");
        if (Health >= _maxHealth)
            return;

        Debug.Log("Not Max Health");
        Health++;
        UIManager.Instance.Heal(Health-1);
        Debug.Log("Healed");
    }
}
