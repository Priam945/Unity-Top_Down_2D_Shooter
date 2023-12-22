using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenSetting : MonoBehaviour
{
    public Image uiSetting;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            uiSetting.gameObject.SetActive(true);
            TogglePause();
        }
    }
    void TogglePause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            uiSetting.gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            uiSetting.gameObject.SetActive(true);
        }
    }
}
