using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    public enum MiniGame {Random, Game1, Game2, Game3}
    [SerializeField]
    private MiniGame minigame;
    
    private void Start()
    {
        Debug.Log(System.Enum.GetValues(typeof(MiniGame)).Length);
        if (minigame == MiniGame.Random) {
            minigame = (MiniGame) Random.Range(1,System.Enum.GetValues(typeof(MiniGame)).Length);
        }
    }

    private void Update()
    {
        
    }
}
