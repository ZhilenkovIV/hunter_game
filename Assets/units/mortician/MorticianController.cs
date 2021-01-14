using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorticianController : MonoBehaviour
{

    private MoveXCommand motion;
    private MorticianStroke stroke;
    private ICommand charge;
    private Animator animator;
    private Rigidbody2D player;
    private Rigidbody2D rb;

    private FollowBehavior follow;

    public float maxSpeed;

    IEnumerator cameraTarget(float time) {
        Camera.current.GetComponent<CameraController>().followObject = gameObject.transform;
        yield return new WaitForSeconds(time);
        Camera.current.GetComponent<CameraController>().followObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        motion = GetComponent<MoveXCommand>();
        stroke = GetComponent<MorticianStroke>();
        charge = GetComponent<MorticianCharge>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        follow = GetComponent<FollowBehavior>();

    }

    private void Update()
    {

        //charge.Execute();
        if (follow.IsInMinDistance)
        {
            stroke.Execute();
        }
    }


}
