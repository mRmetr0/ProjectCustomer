using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineManager : MonoBehaviour
{
    [SerializeField]
    private RadarScript radarPrefab;
    [SerializeField]
    private InstinctRadarScript instinctRadar;
    // Start is called before the first frame update
    void Start()
    {
        if (radarPrefab != null) {
            for (int i = transform.childCount-1; i >= 0; i--) {
                MineScript mine = transform.GetChild(i).GetComponent<MineScript>();
                if (mine != null) {
                    RadarScript radar = Instantiate (radarPrefab);
                    radar.SetTarget(mine);
                }
            }
        } else if (instinctRadar != null) {
            for (int i = transform.childCount-1; i >= 0; i--) {
                MineScript mine = transform.GetChild(i).GetComponent<MineScript>();
                if (mine != null) {
                    InstinctRadarScript instinct = Instantiate (instinctRadar);
                    instinct.SetInstinctTarget(mine);
                }
            }
        }
        GameManager.instance.allMines = transform.childCount;
        enabled = false;
    }
}
