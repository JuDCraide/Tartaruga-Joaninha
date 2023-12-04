using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    private int points;

    public GameObject playerRef { get; private set; }
    public TMPro.TextMeshProUGUI pointsText;

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

    public void addPoint() {
        Debug.Log("POINTS");
        points += 5;
        pointsText.text = points.ToString("D7");
    }
}
