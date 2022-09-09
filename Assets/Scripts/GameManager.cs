using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    void Awake()
    {
        //In case there is more to be done with the gamemanager: add DDOL. Otherwhise leave as it is.
        if (instance == null) {
            instance = this;
            //DontDestroyOnLoad(this);
        } else {
            Destroy (this);
        }
    }
    void OnDestroy() {instance = null;}
}
