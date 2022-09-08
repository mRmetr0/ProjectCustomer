using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarScript : MonoBehaviour
{
    [SerializeField]
    private MineScript mine;
    [SerializeField][Tooltip("The higher the number, the slower the pointer clears when moving away from the mine.")]
    private float alphaChangeMod;
    [SerializeField][Range(0,1)][Tooltip("The higher this number, the earlier the pointer goes invisible")]
    private float alphaZeroRange;
    private PlayerMovement player;
    private float mAlpha;
    private GameObject pointer;
    private Renderer pRenderer;
    [SerializeField]
    private Material pMaterial;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        //pointer = FindObjectOfType<GameObject>();
        pRenderer = GetComponentInChildren<Renderer>();
        //pMaterial = pointer.GetComponent<Renderer>().material;
        //pRenderer.material.color = pMaterial.color;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;

        mAlpha = alphaChangeMod/(transform.position - mine.transform.position).magnitude - alphaZeroRange;
        mAlpha = Mathf.Clamp(mAlpha, 0, 1); //Takes the distance between the player and designated mine, then devides it and clamps it between 0 and 1;
        Debug.Log(this + ":"+mAlpha);

        //Handles Alpha
        Color TextureColor = pRenderer.material.color;
        TextureColor.a = mAlpha;
        pRenderer.material.color = TextureColor;

        //Aims the pointer into the right direction;
        transform.LookAt(new Vector3(mine.transform.position.x, this.transform.position.y, mine.transform.position.z));
        if (mine.GetDifused()) {
            Destroy(this.gameObject);
        }
    }
    public void SetTarget(MineScript setMine) {
        this.transform.parent = null;
        mine = setMine;
    }
}
