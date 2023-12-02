using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public GameObject playerRef {get; private set;}

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this);
        }
        else {
            instance = this;
        }
    }

    void Start() {

    }

    void Update() {

    }

    public void setPlayer(GameObject newPlayer) {
        playerRef = newPlayer;
    }

    public GameObject getPlayer() {
        return playerRef;
    }
}
