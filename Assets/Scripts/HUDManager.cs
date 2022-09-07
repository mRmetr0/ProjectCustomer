using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    GameObject game1, game2, game3;
    PlayerMovement player;
    CameraController cameraControl;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        cameraControl = FindObjectOfType<CameraController>();

        game1 = GameObject.FindWithTag("Game1");
        game1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
