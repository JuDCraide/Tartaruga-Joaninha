using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Hats {
    [SerializeField] private static List<Hat> hats = new List<Hat> { null, null, null, null, null, null, null, null, null, null };
    [SerializeField] public static Hat selectedHat = null;

    public static Hat GetHat(int id) {
        if (hats[id] == null) {
            Debug.Log("null");
        }
        else {
            Debug.Log(hats[id].id.ToString());
        }
        return hats[id];
    }

    public static void SetHat(int id, Hat h) {
        hats[id] = h;
    }

    public static void selectHat(Hat h) {
        if (selectedHat != null) {
            selectedHat.UnselectHat();
        }
        selectedHat = h;
    }

    public static void unselectHat() {
        selectedHat.UnselectHat();
        selectedHat = null;
    }
}
