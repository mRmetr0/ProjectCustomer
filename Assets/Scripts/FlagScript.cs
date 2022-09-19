using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagScript : MonoBehaviour
{
    [SerializeField] GameObject polePos;
    private Vector3 moveDown;

    void Start()
    {
        moveDown = new Vector3(0, .5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position -= moveDown;
        if(this.transform.position.y <= polePos.transform.position.y + 2)
        {
            moveDown = new Vector3(0, 0, 0);
        }
    }
}
