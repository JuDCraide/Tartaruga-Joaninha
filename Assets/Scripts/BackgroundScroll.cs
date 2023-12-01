using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {

    private MeshRenderer mr;
    private Transform cam;
    private Vector3 camStartPos;

    public float scrollSpeed;

    void Start() {
        mr = GetComponent<MeshRenderer>();
        cam = Camera.main.transform;
        camStartPos = cam.position;
    }

    void LateUpdate() {
        float distance = cam.position.x - camStartPos.x;
        transform.position = new Vector3(cam.position.x, transform.position.y, transform.position.z);
        mr.material.mainTextureOffset = new Vector2(distance, 0) * scrollSpeed / 100;
    }
}
