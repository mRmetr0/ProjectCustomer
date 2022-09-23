using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HomeScreenButtonScript : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField]
    private GameObject settingsOverlay;
    [SerializeField]
    GameObject manualOverlay;

    private Button pb;
    public Sprite newSprite;
    public Sprite originalSprite;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        pb = GetComponent<Button>();
    }
    public void PlayButton () {
        SceneManager.LoadScene("Manual");
    }
    public void StartButton()
    {
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
    public void InfoButton()
    {
        manualOverlay.SetActive(true);
    }
    public void ManualOverlay()
    {
        manualOverlay.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        pb.image.sprite = newSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pb.image.sprite = originalSprite;
    }
}
