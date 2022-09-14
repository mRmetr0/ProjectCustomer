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
    private float spawnMax = 20, spawnMin = 1, lengthMax = 1, lengthMin = 0.2f;
    [SerializeField][Range(0,2)]
    private float radarHeight;
    private PlayerMovement player;
    private MineScript mine;
    private float spawnRate, senseLength, tooCloseDist;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + radarHeight-1, player.transform.position.z);

        //Set the activity of the instinct: (length/initensity)
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
        instinct.SetVector3("spawnVel", new Vector3(default, senseLength, default));
        Debug.Log(instinct.GetVector3("spawnVel"));

        //Rotate towards mine:
        transform.LookAt(new Vector3(mine.transform.position.x, this.transform.position.y, mine.transform.position.z));
        if (mine.GetDifused()) {
            Destroy(this.gameObject);
        }
    }
    public void SetInstinctTarget(MineScript setMine) {
        mine = setMine;
    }
}
