using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public enum HUDType {game, result}
    [SerializeField]
    private HUDType type;
    [SerializeField][Tooltip("In seconds.")]
    private float startTime;
    [SerializeField]
    private TextMeshProUGUI mainTextDisplay, subTextDisplay;
    [SerializeField]
    private GameObject settingsOverlay;
    [SerializeField]
    private Image image;
    private bool toFade = false;
    [SerializeField]
    private float fadeSpeed = 0.1f;

    void Start()
    {
        if (type == HUDType.result) {
            mainTextDisplay.text = "Mines difused: "+GameManager.instance.difused.ToString() +" / "+ GameManager.instance.allMines.ToString();
            subTextDisplay.text = "Flags placed: "+GameManager.instance.flags.ToString();
            this.enabled = false;
        }
    }

    private void Update()
    {
        HUDTimer();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            settingsOverlay.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }

        //Something to set toFade to true which will lead into the screen to fade and ultimetally go to the endscene;
        if (toFade) {
            FadeToBlack(fadeSpeed);
        }
    }
    private void HUDTimer() {
        if (startTime > 0) {
            startTime -= Time.deltaTime;
            int displayMin = (int)startTime / 60;
            int displaySec = Mathf.Clamp((int) startTime - 60*displayMin, 0, 60);
            mainTextDisplay.text = displayMin.ToString() +":"+ (displaySec < 10 ? "0"+ displaySec.ToString() : displaySec.ToString());
        } else {
            GameManager.instance.GoToScene("EndScene");
        }
    }

    public void FadeToBlack (float alphaSpeed) {
        Color tempColor = image.color;
        tempColor.a += alphaSpeed;
        image.color = tempColor;
        if (image.color.a >= 1) {GameManager.instance.GoToScene("EndScrollScene");}
    }
}
