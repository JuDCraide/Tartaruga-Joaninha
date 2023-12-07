using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {

    private static int unlockedLevel = 1;
    [SerializeField] private int level;
    [SerializeField] private bool historyLevel = false;

    public TMPro.TextMeshProUGUI levelName;
    
    void Awake() {
        if(level > unlockedLevel) {
            gameObject.GetComponent<Button>().interactable = false;
        }
        if (!historyLevel) {
            levelName.text = "Nível \n"+ level.ToString();
        }
       
    }

    // Update is called once per frame
    void Update() {

    }
}
