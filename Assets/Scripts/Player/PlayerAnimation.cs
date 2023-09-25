using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    private Animator _arcAnimatior;
    private SpriteRenderer _sprite;
    private SpriteRenderer _arcSprite;

    private void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        if (_anim == null)
            Debug.LogError("Animator is NULL");

        _arcAnimatior = transform.GetChild(1).GetComponent<Animator>();
        if (_arcAnimatior == null)
            Debug.LogError("Arc Animator is NULL!");

        _sprite = GetComponentInChildren<SpriteRenderer>();
        if (_sprite == null)
            Debug.LogError("SpriteRenderer is NULL");

        _arcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        if (_arcSprite == null)
            Debug.LogError("Arch Sprite Renderer is NULL!");
    }

    public void MoveAnimation(float movement)
    {
        _anim.SetFloat("Move", Mathf.Abs(movement));
        Flip(movement);
    }

    private void Flip(float movement)
    {
        if (movement > 0)
        {
            _sprite.flipX = false;

            _arcSprite.flipY = false;
            _arcSprite.flipX = false;
            _arcSprite.transform.localPosition = Vector2.right;
        }
        else if (movement < 0)
        {
            _sprite.flipX = true;

            _arcSprite.flipY = true;
            _arcSprite.flipX = true;
            _arcSprite.transform.localPosition = Vector2.left;
        }
    }

    public void JumpAnimation(bool isJumping)
    {
        _anim.SetBool("Jumping", isJumping);
    }

    public void AttackAnimation()
    {
        _anim.SetTrigger("Attack");
        _arcAnimatior.SetTrigger("SwordAnimation");
    }

    public void Hit()
    {
        _anim.SetTrigger("Hit");
    }

    public void Death()
    {
        _anim.SetTrigger("Death");
    }
}
