using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float drag;
    [SerializeField] [Range(1, 10)]
    private float rotationSpeed;

    private Rigidbody rb;
    private Vector3 moveDir;
    private Quaternion rotToDir;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rotToDir = transform.rotation;
    }

    // Update is called once per frame
    private void Update()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        //TODO: player rotation:
        if (moveDir != Vector3.zero) {
            rotToDir = Quaternion.LookRotation(moveDir);
        }
        rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, rotToDir, Time.deltaTime * rotationSpeed);
    }
    private void FixedUpdate()
    {
        rb.AddForce(moveDir.normalized * GetSpeed() * 500f * Time.deltaTime, ForceMode.Force);
    }
    private float GetSpeed() {
        return Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
    }
}
