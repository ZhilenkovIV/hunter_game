using UnityEngine;
using System.Collections;
using System;

public class EventPickUp
{
    public enum ThingType {
        LAMP,
        MONEY
    }

    public static event Action<ThingType> PickUp;

    public static void notify(ThingType thingName) {
        PickUp?.Invoke(thingName);
    }
}
