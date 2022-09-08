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
    private GameObject game1, game2, game3;
    private PlayerMovement player;
    private CameraController cameraControl;
    private void Start()
    {
        //timerText = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<PlayerMovement>();
        cameraControl = FindObjectOfType<CameraController>();

        //
        game1 = GameObject.FindWithTag("Game1");
        game1.SetActive(false);
    }

    private void Update()
    {
        if (startTime > 0) {
            startTime -= Time.deltaTime;
            int displayMin = (int)startTime / 60;
            int displaySec = Mathf.Clamp((int) startTime - 60*displayMin, 0, 60);
            timerText.text = displayMin.ToString() +":"+ (displaySec < 10 ? "0"+ displaySec.ToString() : displaySec.ToString());
        }
    }

    public void GetGame (MineScript.MiniGame minigame) {
        player.enabled = false;
        cameraControl.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        switch (minigame) {
            case (MineScript.MiniGame.Game1):
                game1.SetActive(true);
            break;
        }
    }
    public void ResetGame (GameObject game) {
        Cursor.lockState = CursorLockMode.Locked;

        player.enabled = true;
        cameraControl.enabled = true;

        game.SetActive(false);
    }

    //Functions for game1:
    public void PressButton () {
        ResetGame(game1);
    }
}
