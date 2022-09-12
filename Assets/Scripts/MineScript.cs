using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    [SerializeField]
    private float minDistance;
    [SerializeField]
    private float maxDistance;
    private PlayerMovement player;
    private Vector3 flatDistance;
    private bool difused = false;
    
    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }
    void Start()
    {
        PlayerMovement.onMineCheck += FlagableMine;
    }
    void OnDestroy()
    {
        PlayerMovement.onMineCheck -= FlagableMine;
    }
    void FlagableMine() {
        if (!difused) {
            flatDistance = new Vector3( player.transform.position.x - transform.position.x, 0,  player.transform.position.z - transform.position.z);
            if (flatDistance.magnitude <= maxDistance) {
                GameManager.instance.difused++;
                difused = true;
            }
        }
    }
    public bool GetDifused () {
        return difused;
    }
}
