using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {

    private static int unlockedLevel = 1;
    [SerializeField] private int level;

    public TMPro.TextMeshProUGUI levelName;
    
    void Start() {
        if(level > unlockedLevel) {
            gameObject.GetComponent<Button>().interactable = false;
        }
        levelName.text = level.ToString();
    }

    // Update is called once per frame
    void Update() {

    }
}
