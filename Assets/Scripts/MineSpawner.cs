using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSpawner : MonoBehaviour
{
    [SerializeField] private GameObject startPos;
    [SerializeField] private GameObject endPos;
    [SerializeField] private GameObject mine;
    [SerializeField] private GameObject ground;
    [SerializeField] private GameObject mineManager;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private int maxMines;

    private Vector3 mineSize;
    private Vector3 spawnPos;
    void Start()
    {
        mineSize = new Vector3(3, 3, 3);
        mine.transform.localScale = mineSize;

        for (int i = 0; i < maxMines; i++)
        {
            spawnPos = new Vector3(Random.Range(startPos.transform.position.x, endPos.transform.position.x),
                    Random.Range(startPos.transform.position.y, endPos.transform.position.y),
                    Random.Range(startPos.transform.position.z, endPos.transform.position.z));
            Ray ray;
            RaycastHit hit;
            ray = new Ray(spawnPos, -transform.up);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Debug.Log("ray");
                if (hit.collider.tag == "ground")
                    Debug.Log("spawn");
                Instantiate(mine, hit.point, Quaternion.identity, mineManager.transform);
            }
        }
    }

    void Update()
    {
        
    }
}
