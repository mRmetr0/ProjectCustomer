using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

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
    [SerializeField]
    private int flagAmout;
    [SerializeField]
    private GameObject Pole;
    [SerializeField]
    private LayerMask mask;
    [SerializeField]
    private GameObject polePos;
    [SerializeField]
    SoundManager sounds;

    private float vInput;
    private float hInput;

    private Rigidbody rb;
    private Vector3 moveDir;
    private Vector3 poleOffset = new Vector3(0, 10, 0);
    private Quaternion rotToDir;
    private CameraController camOrientation;
    private HUDManager HUD;
    public static event Action onMineCheck;

    private void Awake()
    {
        HUD = FindObjectOfType<HUDManager>();
        camOrientation = FindObjectOfType<CameraController>();
        rb = GetComponent<Rigidbody>();
        rb.drag = drag;
        rotToDir = transform.rotation;
    }

    private void Start () {
        //Make sure the player starts the game standing on the ground:
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, mask)) {
            if (hit.collider != null) {
                this.transform.position = new Vector3(transform.position.x, hit.point.y + (transform.localScale.y/2)+1, transform.position.z);
            }
        }
    }

    private void Update()
    {
        PlayerInput();
        RotatePlayer();
        FlagPlacement();
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log(sounds.sfxClips[4]);
            sounds.sfxClips[4].Play();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            Debug.Log(sounds.sfxClips[4] + "stop");
            sounds.sfxClips[4].Stop();
        }
    }
    private void FixedUpdate()
    {
        rb.AddForce(moveDir.normalized * GetSpeed() * 500f * Time.deltaTime, ForceMode.Force);
        rb.AddForce(new Vector3(0, -20f, 0));
    }

    private void FlagPlacement () {
        if (Input.GetKeyDown(KeyCode.E) && flagAmout>0) {
            flagAmout--;
            GameManager.instance.flags++;
            onMineCheck.Invoke();
            Pole.transform.position = polePos.transform.position + poleOffset;
            Instantiate(Pole);
        }
    }

    private float GetSpeed() {
        return Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
    }

    private void RotatePlayer() {
        if (moveDir != Vector3.zero) {
            rotToDir = Quaternion.LookRotation(moveDir);
            //rotToDir = Quaternion.LookRotation(new Vector3(moveDir.x, 0, moveDir.z));
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
