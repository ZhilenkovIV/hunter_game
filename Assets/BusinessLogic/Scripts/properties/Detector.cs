using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
	public LayerMask layerMask;
	public event System.Action Enter;
	public event System.Action Exit;
	public event System.Action Stay;

	private bool CheckLayer(Collider2D other){
		int layerColl = other.gameObject.layer;
        return ((1 << layerColl) & layerMask.value) != 0;
	}


    void OnTriggerEnter2D(Collider2D other){
    	if(Enter != null && CheckLayer(other)){
    		Enter();
    	}
    }

    void OnTriggerStay2D(Collider2D other){
    	if(Stay != null && CheckLayer(other)){
    		Stay();
    	}
    }

    void OnTriggerExit2D(Collider2D other){
    	if(Exit != null && CheckLayer(other)){
    		Exit();
    	}
    }
}
