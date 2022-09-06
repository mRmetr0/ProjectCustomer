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

    private float vInput;
    private float hInput;

    private Rigidbody rb;
    private Vector3 moveDir;
    private Quaternion rotToDir;
    private CameraController camOrientation;

    private void Awake()
    {
        camOrientation = FindObjectOfType<CameraController>();
        rb = GetComponent<Rigidbody>();
        rb.drag = drag;
        rotToDir = transform.rotation;
    }

    private void Update()
    {
        PlayerInput();
        RotatePlayer();
    }
    private void FixedUpdate()
    {
        rb.AddForce(moveDir.normalized * GetSpeed() * 500f * Time.deltaTime, ForceMode.Force);
    }

    private float GetSpeed() {
        return Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
    }

    private void RotatePlayer() {
        if (moveDir != Vector3.zero) {
            rotToDir = Quaternion.LookRotation(moveDir);
        }
        rb.transform.rotation = Quaternion.Slerp(rb.transform.rotation, rotToDir, Time.deltaTime * rotationSpeed);
    }

    private void PlayerInput () {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        //moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        moveDir = camOrientation.transform.forward * vInput + camOrientation.transform.right * hInput;
    }
}
