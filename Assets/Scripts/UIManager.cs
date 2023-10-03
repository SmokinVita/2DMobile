using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    { 
        get
        {
            if (_instance == null)
                Debug.LogError("UIManager is NULL");

                return _instance; 
        } 
    }

    [SerializeField] private GameObject _shopMenu;
    [SerializeField] private TMP_Text _openShopText;
    [SerializeField] private TMP_Text _inShopDiamondAmountText;
    [SerializeField] private TMP_Text _inGameDiamondAmountText;
    [SerializeField] private Image _selectionImage;
    [SerializeField] private Image[] _healthUnits;

    private void Awake()
    {
        _instance = this;
    }

    public void ShowShopMenu(bool isOpen)
    {
        _shopMenu.SetActive(isOpen);
    }

    public void EnableShopText(bool nearShop)
    {
        _openShopText.gameObject.SetActive(nearShop);
    }

    public void UpdateDiamondAmount(int diamonds)
    {
        _inShopDiamondAmountText.SetText($"{diamonds} G");
        _inGameDiamondAmountText.SetText($"{diamonds}");
    }

    public void UpdateShopSelection(float yPos)
    {
        _selectionImage.rectTransform.localPosition = new Vector3(_selectionImage.rectTransform.localPosition.x, yPos);
    }

    public void UpdateLives(int livesRemaining)
    {
        for (int i = 0; i < _healthUnits.Length; i++)
        {
            if(i == livesRemaining)
                _healthUnits[i].enabled = false;
        }
    }

    //maybe use later
    //public void Heal(int livesRegained)
    //{
    //    for (int i = 0; i < _healthUnits.Length; i++)
    //    {
    //        if(i == livesRegained)
    //            _healthUnits[i].enabled = true;
    //    }
    //}
}
