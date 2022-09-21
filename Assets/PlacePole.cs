using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePole : MonoBehaviour
{
    [SerializeField]
    private GameObject ground;
    [SerializeField]
    private Vector3 moveDown;
    private Vector3 currentPos;

    void Start()
    {
        currentPos = this.transform.position;
    }
    void FixedUpdate()
    {
        this.transform.position = currentPos - moveDown * Time.deltaTime;
        currentPos = this.transform.position;
        if (currentPos.y < ground.transform.position.y)
        {
            moveDown = new Vector3(0, 0, 0);
        }
    }
}
