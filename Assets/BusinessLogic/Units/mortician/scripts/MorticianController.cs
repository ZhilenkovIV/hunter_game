using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorticianController : MonoBehaviour
{

    private MorticianStroke stroke;
    private ICommand charge;

    private FollowBehavior follow;

    IEnumerator cameraTarget(float time) {
        Camera.current.GetComponent<CameraController>().followObject = gameObject.transform;
        yield return new WaitForSeconds(time);
        Camera.current.GetComponent<CameraController>().followObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        stroke = GetComponent<MorticianStroke>();
        charge = GetComponent<MorticianCharge>();
        follow = GetComponent<FollowBehavior>();
    }

    private void Update()
    {
        if (follow.IsFollowing)
        {
            //charge.Execute();
            if (follow.IsInMinDistance)
            {
                stroke.Execute();
            }
            else
            {
                charge.Execute();
            }
        }
    }


}
