using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarScript : MonoBehaviour
{
    [SerializeField]
    private MineScript mine;
    private PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        transform.LookAt(new Vector3(mine.transform.position.x, this.transform.localScale.y, mine.transform.position.z));
        if (mine.GetDifused()) {
            Destroy(gameObject);
        }
    }
    public void SetTarget(MineScript setMine) {
        this.transform.parent = null;
        mine = setMine;
    }
}
