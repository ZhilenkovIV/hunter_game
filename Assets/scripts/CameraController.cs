using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float dumping = 1.5f;
    public Vector2 offset = new Vector2(2f, 1f);
    public bool isLeft;
    public Transform followObject;

    public Vector2 sizeScene;
    public Vector2 offsetScene;
    
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (followObject) {

            Vector3 target;
            if (isLeft)
            {
                target = new Vector3(followObject.position.x - offset.x, followObject.position.y + offset.y, transform.position.z);
            }
            else {
                target = new Vector3(followObject.position.x + offset.x, followObject.position.y + offset.y, transform.position.z);
            }


            Vector3 currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
            transform.position = currentPosition;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(offsetScene, sizeScene);   
    }
}
