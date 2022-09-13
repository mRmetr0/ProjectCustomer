using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class InstinctRadarScript : MonoBehaviour
{
    [SerializeField]
    private VisualEffect instinct;
    [SerializeField]
    private float spawnMod, lengthMod;
    [SerializeField]
    private float spawnMax = 20, spawnMin = 1, lengthMax = 1, lengthMin = 0.3f;
    [SerializeField]
    private float tooCloseDist = 3, radarHeight;
    private PlayerMovement player;
    private MineScript mine;
    private float spawnRate;
    private float senseLength;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        if (radarHeight <=0) {radarHeight = player.transform.position.y;}
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x, radarHeight, player.transform.position.z);

        Vector2 flatDist = new Vector2(mine.transform.position.x - transform.position.x, mine.transform.position.z - transform.position.z);
        if (flatDist.magnitude <= tooCloseDist) {
            spawnRate = 0;
        } else {
            spawnRate = spawnMod-flatDist.magnitude;
            spawnRate = Mathf.Clamp(spawnRate, spawnMin, spawnMax);
            senseLength = flatDist.magnitude/lengthMod;
            senseLength = Mathf.Clamp(senseLength, lengthMin,lengthMax);
        }
        instinct.SetInt("spawn rate", (int)spawnRate);
        instinct.transform.localScale = new Vector3(transform.localScale.x, senseLength, transform.localScale.z);


        transform.LookAt(new Vector3(mine.transform.position.x, this.transform.position.y, mine.transform.position.z));
        if (mine.GetDifused()) {
            Destroy(this.gameObject);
        }
    }
    public void SetInstinctTarget(MineScript setMine) {
        mine = setMine;
    }
}
