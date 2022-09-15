using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public enum HUDType {game, result}
    [SerializeField]
    private HUDType type;
    [SerializeField][Tooltip("In seconds.")]
    private float startTime;
    [SerializeField]
    private TextMeshProUGUI mainTextDisplay, subTextDisplay;

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
}
