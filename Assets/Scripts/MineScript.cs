using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    public enum miniGame {Random, Game1, Game2, Game3}
    [SerializeField]
    private miniGame minigame;
    
    private void Start()
    {
        Debug.Log(System.Enum.GetValues(typeof(miniGame)).Length);
        if (minigame == miniGame.Random) {
            minigame = (miniGame) Random.Range(1,System.Enum.GetValues(typeof(miniGame)).Length);
        }
    }

    private void Update()
    {
        
    }
}
