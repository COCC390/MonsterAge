using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region private variable

    private GameObject _player;

    #endregion

    #region unity method

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
    }

    #endregion

}
