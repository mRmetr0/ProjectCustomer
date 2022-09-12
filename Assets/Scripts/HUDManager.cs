using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField][Tooltip("In seconds.")]
    private float startTime;
    [SerializeField]
    private TextMeshProUGUI timerText;

    private void Update()
    {
        if (startTime > 0) {
            startTime -= Time.deltaTime;
            int displayMin = (int)startTime / 60;
            int displaySec = Mathf.Clamp((int) startTime - 60*displayMin, 0, 60);
            timerText.text = displayMin.ToString() +":"+ (displaySec < 10 ? "0"+ displaySec.ToString() : displaySec.ToString());
        } else {
            if (GameManager.instance.GetWin()) {
                GameManager.instance.GoToScene("WinScene");
            } else {
                GameManager.instance.GoToScene("LoseScene");
            }
        }
    }
}
