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
    [SerializeField]
    private GameObject Pole;
    private HUDManager HUD;

    private PlayerMovement player;
    private Vector3 flatDistance;
    private bool lost = false, difused = false;
    
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
        flatDistance = new Vector3( player.transform.position.x - transform.position.x, 0,  player.transform.position.z - transform.position.z);
        if (flatDistance.magnitude < minDistance && !lost) {
            lost = true;
            Debug.Log("Player Lost");        
            Invoke(nameof (BackToTitle), 2f);    
        } else if (flatDistance.magnitude < maxDistance && Input.GetKeyDown(KeyCode.E) && !difused && !lost) {
            difused = true;
            Pole.transform.position = transform.position;
            Instantiate(Pole);
            Destroy(this);
            //HUD.GetGame(minigame);            //Scrapped for now
        }
    }

    private void BackToTitle () {
        SceneManager.LoadScene(0);
    }
    public bool GetDifused () {
        return difused;
    }
}
