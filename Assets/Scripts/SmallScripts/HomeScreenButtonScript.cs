using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreenButtonScript : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void StartButton () {
        SceneManager.LoadScene(1);
    }
    public void ExitButton () {
        Application.Quit();
    }
    public void CampaignButton () {
        Application.OpenURL("https://www.amnesty.org/en/what-we-do/arms-control/");
    }
    public void SettingButton () {
        SceneManager.LoadScene("SettingsScene");
    }
}
