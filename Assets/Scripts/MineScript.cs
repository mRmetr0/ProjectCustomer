using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MineScript : MonoBehaviour
{
    public enum MiniGame {Random, Game1, Game2, Game3}
    [SerializeField]
    private MiniGame minigame;
    [SerializeField]
    private float minDistance;
    [SerializeField]
    private float maxDistance;
    private HUDManager HUD;

    private PlayerMovement player;
    private Vector3 distance;
    private bool lost, difused = false;
    
    private void Awake()
    {
        HUD = FindObjectOfType<HUDManager>();
        player = FindObjectOfType<PlayerMovement>();

        if (minigame == MiniGame.Random) {
            minigame = (MiniGame) Random.Range(1,System.Enum.GetValues(typeof(MiniGame)).Length);
        }
    }

    void Start()
    {
        GameManager.instance.GetMinesList().Add(this);
    }

    private void Update()
    {
        distance = player.transform.position - transform.position;
        if (distance.magnitude < minDistance && !lost) {
            lost = true;
            Debug.Log("Player Lost");        
            Invoke(nameof (BackToTitle), 5f);    
        } else if (distance.magnitude < maxDistance && Input.GetKeyDown(KeyCode.E) && !difused) {
            difused = true;
            Destroy(this);
            HUD.GetGame(minigame);
        }
    }

    private void BackToTitle () {
        SceneManager.LoadScene(0);
    }
    public bool GetDifused () {
        return difused;
    }
}
