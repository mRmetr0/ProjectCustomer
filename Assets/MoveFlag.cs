using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFlag : MonoBehaviour
{
    [SerializeField] GameObject terrain;

    [SerializeField] Vector3 moveDown;
    void Start()
    {
        moveDown = new Vector3(0, .1f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position -= moveDown;
    }
    public void OnCollisionEnter(Collision other)
    {
        Debug.Log("collides");
        moveDown = new Vector3(0, 0, 0);
        if (terrain.gameObject.tag == "ground")
        {
            Debug.Log("stop");
            moveDown = new Vector3(0, 0, 0);
        }
    }
}
