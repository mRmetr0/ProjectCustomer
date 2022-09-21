using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSpawner : MonoBehaviour
{
    [SerializeField] private GameObject startPos;
    [SerializeField] private GameObject endPos;
    [SerializeField] private List<GameObject> objects;
    [SerializeField] private GameObject environmentManager;
    [SerializeField] private GameObject mineManager;
    [SerializeField] private LayerMask groundMask;
    [Space]
    [SerializeField] private int maxMines;
    [SerializeField] private int maxBushes;
    [SerializeField] private int maxDryGrass;
    [SerializeField] private int maxGreenGrass;
    [SerializeField] private int maxRocks;
    [SerializeField] private int maxTrees;

    private int spawnAmount;

    private Vector3 mineSize;
    private Vector3 spawnPos;
    void Start()
    {
        mineSize = new Vector3(3, 3, 3);
        objects[0].transform.localScale = mineSize;
        for(int i = 0; i < objects.Count; i++)
        {
            if(i == 0)
            {
                spawnAmount = maxMines;
            }
            if (i == 1 || i == 2 || i == 3 || i == 4 || i == 5 || i == 6)
            {
                spawnAmount = maxBushes;
            }
            if (i == 7 || i == 8 || i == 9 || i == 10 || i == 11)
            {
                spawnAmount = maxDryGrass;
            }
            if (i == 12)
            {
                spawnAmount = maxGreenGrass;
            }
            if (i == 13 || i == 14 || i == 15 || i == 16)
            {
                spawnAmount = maxRocks;
            }
            if (i == 17)
            {
                spawnAmount = maxTrees;
            }
            for (int ii = 0; ii < spawnAmount; ii++)
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
                    if (i == 0)
                    {
                        Instantiate(objects[0], hit.point - new Vector3(0, Random.Range(0.4f, 0.7f), 0), Quaternion.identity, mineManager.transform);
                    }
                    else
                    {
                        Instantiate(objects[i], hit.point, Quaternion.identity, environmentManager.transform);
                    }
                }
            }
        }
    }

    void Update()
    {
        
    }
}
