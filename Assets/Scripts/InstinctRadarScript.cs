using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using System;

public class InstinctRadarScript : MonoBehaviour
{
    [SerializeField]
    private VisualEffect instinct;
    [SerializeField]
    private float spawnMod, lengthMod;
    [SerializeField]
    private float spawnMax = 20, spawnMin = 1, lengthMax = 1, lengthMin = 0.2f, maxSpawnAngle = 3, angleMod;
    [SerializeField][Range(0,2)]
    private float radarHeight;
    private PlayerMovement player;
    private MineScript mine;
    private float spawnRate, senseLength, tooCloseDist, spawnAngle;
    private bool nearMine;

    public static event Action onPlayerNear;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        InstinctRadarScript.onPlayerNear += NearMine;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + radarHeight-1, player.transform.position.z);

        //Set the activity of the instinct: (length/initensity)
        Vector2 flatDist = new Vector2(mine.transform.position.x - transform.position.x, mine.transform.position.z - transform.position.z);

        if (flatDist.magnitude <= tooCloseDist) {
            //spawnRate = 0;
            onPlayerNear?.Invoke();
        } else {
            float edgeOfMineRadius = flatDist.magnitude - tooCloseDist;
            spawnRate = spawnMod-edgeOfMineRadius ;
            spawnRate = Mathf.Clamp(spawnRate, spawnMin, spawnMax);

            senseLength = edgeOfMineRadius/lengthMod;
            senseLength = Mathf.Clamp(senseLength, lengthMin,lengthMax);

            spawnAngle = angleMod/edgeOfMineRadius;
            spawnAngle = Mathf.Clamp(spawnAngle, 0, maxSpawnAngle);
        }

        instinct.SetInt("spawnRate", (int)spawnRate);
        instinct.SetVector3("spawnVelA", new Vector3(spawnAngle, senseLength, default));
        instinct.SetVector3("spawnVelB", new Vector3(-spawnAngle, senseLength, default));

        //Debug.Log("SR: "+(int)spawnRate+" SV: "+senseLength+" FD: "+flatDist.magnitude + " SA: "+spawnAngle);
        //Debug.Log(flatDist.magnitude);

        //Rotate towards mine:
        transform.LookAt(new Vector3(mine.transform.position.x, this.transform.position.y, mine.transform.position.z));
        if (mine.GetDifused()) {
            Destroy(this.gameObject);
        }
    }
    private void OnDestroy()
    {
        InstinctRadarScript.onPlayerNear -= NearMine;
    }
    private void NearMine() {
        spawnRate = 0;
    }
    public void SetInstinctTarget(MineScript setMine) {
        mine = setMine;
        tooCloseDist = mine.GetMaxDist();
    }
}
