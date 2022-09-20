using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSound : MonoBehaviour
{
    [SerializeField] SoundManager sounds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Grass")
        {
            Debug.Log("play sound");
            sounds.sfxClips[1].Play();
        }
    }
}
