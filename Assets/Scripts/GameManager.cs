using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int difusedMines;
    private int maxMines;
    public int difused {
        get{return difusedMines;}
        set{difusedMines = value;}
    }
    public int allMines {
        set {maxMines = value;}
    }
    
    void Awake()
    {
        //In case there is more to be done with the gamemanager: add DDOL. Otherwhise leave as it is.
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        } else {
            Destroy (this);
        }
    }
    public bool GetWin() {
        return difusedMines >= maxMines;
    }
    public void GoToScene(int scene) {
        SceneManager.LoadScene(scene);
    }
    public void GoToScene(string scene) {
        SceneManager.LoadScene(scene);
    }
}
