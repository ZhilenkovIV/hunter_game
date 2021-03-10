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
        pressPlate.Enter += () => { lightTest.enabled = true; };
        pressPlate.Exit += () => { lightTest.enabled = false; };
    }

}
