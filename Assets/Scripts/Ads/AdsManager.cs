using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{

    [SerializeField] private Button _showAdButton;
    [SerializeField] private string _androidAdUnitID = "Rewarded_Android";
    [SerializeField] private string _iOSAdUnityID = "Rewarded_iOS";
    private string _adUnitID = null;

    [SerializeField] private Player _player;

    private void Awake()
    {
#if UNITY_IOS
        _adUnitID = _iOSAdUnityID;
#elif UNITY_ANDROID
        _adUnitID = _androidAdUnitID;
#endif

        _showAdButton.interactable = false;

    }

    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + _adUnitID);
        Advertisement.Load(_adUnitID, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Ad loaded: " + placementId);

        if (placementId.Equals(_adUnitID))
        {
            _showAdButton.onClick.AddListener(ShowAd);
            _showAdButton.interactable = true;
        }
    }

    public void ShowAd()
    {
        _showAdButton.interactable = false;
        Advertisement.Show(_adUnitID, this);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {placementId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {placementId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId){    }

    public void OnUnityAdsShowClick(string placementId){    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {

        Debug.Log($"Completion State is: {showCompletionState}");
        if (placementId.Equals(_adUnitID) && showCompletionState == UnityAdsShowCompletionState.COMPLETED)  //showCompletionState.Equals(UnityAdsCompletionState.COMPLETED))
        {
            Debug.Log("Unity has rewarded the player!!");
            GameManager.Instance.player.AddDiamonds(100);

        }
        else if (placementId.Equals(_adUnitID) && showCompletionState == UnityAdsShowCompletionState.SKIPPED) //showCompletionState.Equals(UnityAdsCompletionState.SKIPPED))
        {
            Debug.Log("Skipped Ad");
        }
        else if (placementId.Equals(_adUnitID) && showCompletionState == UnityAdsShowCompletionState.UNKNOWN) //showCompletionState.Equals(UnityAdsCompletionState.UNKNOWN))
        {
            Debug.Log("Something didn't go right");
        }
    }
}
