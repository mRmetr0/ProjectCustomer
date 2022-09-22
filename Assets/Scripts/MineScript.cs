using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    [SerializeField][Tooltip("This is the closest the player can get before the instinct deactivates.")]
    private float maxDistance;
    [SerializeField][Tooltip("This is the closest the player needs to be to plant a flag.")]
    private float difuseDistance;
    [SerializeField][Tooltip("If player places flag in this radius, the mine goes off.")]
    private float deathDistance;
    [SerializeField]
    private float minDistance;

    private Vector3 defusePos;
    private PlayerMovement player;
    private Vector3 flatDistance;
    private bool handled = false;
    
    private void Awake()
    {
        defusePos = new Vector3(0f, .1f, 0f);
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
        if (!handled) {
            flatDistance = new Vector3( player.transform.position.x - transform.position.x, 0,  player.transform.position.z - transform.position.z);
            if (flatDistance.magnitude <= deathDistance) {      //Puts flag on mine and detonates;
                GameManager.instance.GoToScene("EndScrollScene");
            } else if (flatDistance.magnitude <= difuseDistance) {  //Close enough to diffuse the bomb:
                handled = true;
                GameManager.instance.difused++;
                Debug.Log("diffused");
            } else if (flatDistance.magnitude <= maxDistance) { //Too far away to diffuse but still kills instinct:
                handled = true;
            }
            
        }
    }
    public bool GetDifused () {
        return handled;
    }
    public float GetMaxDist() {
        return maxDistance;
    }
}
