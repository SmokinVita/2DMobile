using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("GameManager is NULL!");

            return _instance;
        }
    }

    public bool _hasFlamingSword {  get; set; }
    public bool _hasBootsOfFlight { get; set; }
    public bool _hasCastleKey { get; set; }

    private void Awake()
    {
        _instance = this;
    }
}
