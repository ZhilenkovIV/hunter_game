﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInfo : MonoBehaviour
{
    static public GameObject player;
    // Start is called before the first frame update
    static GameObject getPlayer() {
        return player;
    }

    static void setPlayer() {

    }
}