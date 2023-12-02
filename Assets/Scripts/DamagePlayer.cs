using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if(playerHealth != null){
                playerHealth.takeDamage();
            }
        }
    }
}
