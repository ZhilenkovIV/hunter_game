using UnityEngine;
using System.Collections;

public class LightTest : MonoBehaviour
{
    private Light lightTest;
    public PressPlate pressPlate;

    // Use this for initialization
    void Start()
    {
        lightTest = GetComponent<Light>();
        lightTest.enabled = false;
        pressPlate.notify += (s) => { if (s == "enter") { lightTest.enabled = true; } };
        pressPlate.notify += (s) => { if (s == "exit") { lightTest.enabled = false; } };
    }

}
