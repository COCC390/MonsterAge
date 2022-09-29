using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private GameObject _soundSettingPopup;
    [SerializeField] private GameObject _soundButton;
    void Start()
    {
        _soundSettingPopup.SetActive(false);
        _soundButton.SetActive(true);
    }

    public void OnClickShowPopup()
    {
        _soundSettingPopup.SetActive(true);
        _soundButton.SetActive(false);
    }

    public void OnClickHidePopup()
    {
        _soundSettingPopup.SetActive(false);
        _soundButton.SetActive(true);
    }
}
