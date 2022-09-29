using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    [SerializeField] private Text _buttonText;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnClickOnOffSoundButton()
    {
        Debug.Log("On");
        if (_buttonText.text.Equals("On"))
        {
            Debug.Log("On");
            _buttonText.text = "Off";
        }
        else
        {
            Debug.Log("Off");
            _buttonText.text = "On";
        }
    }
}
