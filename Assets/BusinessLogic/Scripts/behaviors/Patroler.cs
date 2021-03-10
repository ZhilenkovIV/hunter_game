using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroler : MonoBehaviour
{
	public float speed;
    private float currentSpeed;

	private IMotion motion;
    public float time;
    public float wait;
    private float currentTime;
	public Vector2 point;

    // Start is called before the first frame update
    void Start()
    {
        motion = GetComponent<IMotion>();
        point = transform.position;
        currentSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
    	if(currentTime < time){
    		motion.SetSpeedX(currentSpeed);
    	} else if (currentTime < time + wait){
    		motion.SetSpeedX(0);
    	} else{
    		currentTime = 0;
    		currentSpeed *= -1;
    		motion.SetSpeedX(2 * currentSpeed);
    	}
        motion.Execute();
        currentTime += Time.deltaTime;
    }
}
