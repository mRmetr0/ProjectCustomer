using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineManager : MonoBehaviour
{
    [SerializeField]
    private RadarScript radarPrefab;
    private List<MineScript> mines;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = transform.childCount-1; i >= 0; i--) {
            MineScript mine = transform.GetChild(i).GetComponent<MineScript>();
            if (mine != null) {
                RadarScript radar = Instantiate (radarPrefab);
                radar.SetTarget(mine);
            }
        }
        //Destroy(gameObject);
    }
}
