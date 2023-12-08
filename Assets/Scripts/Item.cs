using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

enum ItemTypes {
    health,
    life,
    money,
    end
}

public class Item : MonoBehaviour {

    [SerializeField] private ItemTypes type;
    [SerializeField] private HealthBar healthBar;

    [SerializeField] private Sound itemCollectedSound;

    void Start() {

    }

    void Update() {

    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            if (type == ItemTypes.health) {
                PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null && !playerHealth.hasFullHealth()) {
                    playerHealth.addHealth();
                    AudioManager.instance.Play(itemCollectedSound);
                    Destroy(gameObject);
                }
            }
            else if (type == ItemTypes.life) {
                if (!healthBar.hasUnlockedAllCointainer()) {
                    healthBar.unlockContainer();
                    AudioManager.instance.Play(itemCollectedSound);
                    Destroy(gameObject);
                }
            }
            else if (type == ItemTypes.money) {
                GameManager.instance.addMoney(5);
                AudioManager.instance.Play(itemCollectedSound);
                Destroy(gameObject);
            }
            else if(type == ItemTypes.end) {
                GameManager.EndLevel(GameManager.currentLevel);
            }

        }
    }
}
