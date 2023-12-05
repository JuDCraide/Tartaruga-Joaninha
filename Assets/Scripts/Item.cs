using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

enum ItemTypes {
    health,
    life,
    points
}

public class Item : MonoBehaviour {

    [SerializeField] private ItemTypes type;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameManager gm;

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
                    Destroy(gameObject);
                }
            }
            else if (type == ItemTypes.life) {
                if (!healthBar.hasUnlockedAllCointainer()) {
                    healthBar.unlockContainer();
                    Destroy(gameObject);
                }
            }
            else if (type == ItemTypes.points) {
                gm.addPoint();
                Destroy(gameObject);
            }
        }
    }
}
