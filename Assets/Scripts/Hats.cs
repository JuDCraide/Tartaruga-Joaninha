using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public static class Hats {
    [SerializeField] public static Hat[] hats = new Hat[] { null, null, null, null, null, null, null, null, null, null };
    [SerializeField] public static Hat selectedHat { get; private set; } = null;
    
    public static Hat GetHat(int id) {
        return Hats.hats[id];
    }

    public static void SetHat(int id, Hat h) {
        Hats.hats[id] = h;
    }

    public static void selectHat(Hat h) {
        if (Hats.selectedHat != null) {
            Hats.selectedHat.UnselectHat();
        }
        Hats.selectedHat = h;
    }

    public static void unselectHat() {
        if (Hats.selectedHat != null) {
            Hats.selectedHat.UnselectHat();
            Hats.selectedHat = null;
        }
    }
}
