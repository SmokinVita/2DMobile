using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Chest : MonoBehaviour
{
    private Animator _anim;
    [SerializeField] private Player _player;
    private bool _inChestArea;
    private bool _hasOpenedChest;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("Animator is NULL!");
        }
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) || CrossPlatformInputManager.GetButtonDown("A_Button")) && !_hasOpenedChest && _inChestArea)
        {
            _anim.SetTrigger("Open");
            _player.AddDiamonds(50);
            _player.Heal();
            _hasOpenedChest = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.EnableChestText();
            _inChestArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.DisableChestText();
            _inChestArea = false;
        }
    }
}
