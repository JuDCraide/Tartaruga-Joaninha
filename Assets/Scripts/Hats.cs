using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public static class Hats {
    [SerializeField] public static List<Hat> hats = new List<Hat> { null, null, null, null, null, null, null, null, null, null };
    [SerializeField] public static Hat selectedHat { get; private set; } = null;

    // static Hats() {
    //     Hat hat = new();
    //     Hats.hats = new List<Hat> { null, null, null, null, null, null, null, null, null, null };
    // }

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
        Hats.selectedHat.UnselectHat();
        Hats.selectedHat = null;
    }
}
