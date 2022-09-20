using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreenButtonScript : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsOverlay;
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
        Application.OpenURL("http://www.icbl.org/en-gb/home.aspx");
    }
    public void SettingButton () {
        SceneManager.LoadScene(3);
    }
    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }
    public void BackButtonInGame()
    {
        settingsOverlay.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
