using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressPlateLog : MonoBehaviour
{
	public PressPlate pressPlate;
    // Start is called before the first frame update
    void Start()
    {
        pressPlate.Enter += ()=> Debug.Log("enter");
        pressPlate.Stay += ()=> Debug.Log("stay");
        pressPlate.Exit += ()=> Debug.Log("exit");
    }

}
