using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private RadarScript radarPrefab;
    private List<MineScript> mines;
    public static GameManager instance;
    
    void Awake()
    {
        //In case there is more to be done with the gamemanager: add DDOL. Otherwhise leave as it is.
        if (instance == null) {
            instance = this;
            mines = new List<MineScript>();
            //DontDestroyOnLoad(this);
        } else {
            Destroy (this);
        }
    }
    void OnDestroy() {instance = null;}
    void Start()
    {
        foreach (MineScript mine in mines) {
            RadarScript radar = Instantiate (radarPrefab);
            radar.SetTarget(mine);
        }
        Debug.Log(mines.Count);
    }
    
    public List<MineScript> GetMinesList () {
        return mines;
    }
}
