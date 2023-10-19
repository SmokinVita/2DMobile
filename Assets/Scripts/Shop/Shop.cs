using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Shop : MonoBehaviour
{

    //variable for currentItemSelected
    private int _selectedItem;
    private int _currentItemCost;
    [SerializeField]
    private Player _player;
    private bool _isInShop;

    [SerializeField] private AdsManager _adsManager;

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) || CrossPlatformInputManager.GetButtonDown("A_Button")) && _isInShop == true)
        {
            UIManager.Instance.ShowShopMenu(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            UIManager.Instance.EnableShopText(true);
            _isInShop = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.EnableShopText(false);
            UIManager.Instance.ShowShopMenu(false);
            _isInShop = false;
        }
    }

    public void SelectItem(int item)
    {
        //0 = flame sword
        //1 = boots of flight
        //2 = key to castle
        switch (item)
        {
            case 0:
                UIManager.Instance.UpdateShopSelection(77f);
                _currentItemCost = 200;
                break;
            case 1:
                UIManager.Instance.UpdateShopSelection(-24);
                _currentItemCost = 400;
                break;
            case 2:
                UIManager.Instance.UpdateShopSelection(-129);
                _currentItemCost = 100;
                break;
        }
        _selectedItem = item;
    }

    //Buy ItemSelected
    //check if playergems is greater than or equal to item cost.
    //if it is, then awarditem (subtract Gems.)
    //else close shop.
    public void BuyItem()
    {

        if (_player.CurrentDiamonds() >= _currentItemCost)
        {
            Debug.Log("Bought Flame Sword!");
            switch (_selectedItem)
            {
                case 0:
                    GameManager.Instance._hasFlamingSword = true;
                    break;
                case 1:
                    GameManager.Instance._hasBootsOfFlight = true;
                    break;
                case 2:
                    GameManager.Instance._hasCastleKey = true;
                    break;
            }

            _player.SubtractDiamonds(_currentItemCost);
            UIManager.Instance.ShowShopMenu(false);
        }
        else
            UIManager.Instance.ShowShopMenu(false);

    }
}
