using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorticianController : MonoBehaviour
{

    IEnumerator cameraTarget(float time) {
        Camera.current.GetComponent<CameraController>().followObject = gameObject.transform;
        yield return new WaitForSeconds(time);
        Camera.current.GetComponent<CameraController>().followObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        EventPickUp.PickUp += (s) =>
        {
            if (s == "fall")
            {
                GetComponent<Animator>().SetTrigger("dig");
                //StartCoroutine(cameraTarget(2));
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
