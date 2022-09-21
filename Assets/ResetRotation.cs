using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRotation : MonoBehaviour
{
    [SerializeField] Vector3 posFix;
    // Start is called before the first frame update
    void Start()
    {
        Quaternion rotationFix = new Quaternion(0, 0, 0, 0);
        this.transform.rotation = rotationFix;
        this.transform.position += posFix;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
