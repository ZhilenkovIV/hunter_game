using UnityEngine;
using System.Collections;
using System;

public class EventPickUp
{
    public static event Action<string> PickUp;

    public static void notify(string thingName) {
        PickUp?.Invoke(thingName);
    }
}
