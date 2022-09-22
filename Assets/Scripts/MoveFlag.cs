using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFlag : MonoBehaviour
{
    [SerializeField] GameObject terrain;

    [SerializeField] Vector3 moveDown;

    [SerializeField]
    LayerMask mask;

    [SerializeField]
    AudioSource sfx;
    void Start()
    {
        this.transform.Rotate(new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-180.0f, 180.0f), Random.Range(-10.0f, 10.0f)));
        if (moveDown.y <= 0) {moveDown = new Vector3(0, .1f, 0);}
    }

    // Update is called once per frame
    void Update()
    {
        if (!FallCheck()) {

        this.transform.position -= moveDown * Time.deltaTime;
        }
    }
    // public void OnCollisionEnter(Collision other)
    // {
    //     Debug.Log("collides");
    //     moveDown = new Vector3(0, 0, 0);
    //     if (terrain.gameObject.tag == "ground")
    //     {
    //         Debug.Log("stop");
    //         moveDown = new Vector3(0, 0, 0);
    //     }
    // }

    bool FallCheck () {
        if (Physics.Raycast(gameObject.transform.position, Vector3.down, 0.7f, mask)) {
            Debug.Log("RayCast HIT!");
            sfx.Play();
            Destroy(this);
            moveDown *= 0;
            return true;
        }
        return false;
    }
}
