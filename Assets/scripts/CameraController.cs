using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float dumping = 1.5f;
    public Transform followObject;

    public Vector2 maxOffset;

    private Vector2 destination;

    public Rect sceneArea;

    private Vector2 cameraWorldHalfSize;

    
    // Start is called before the first frame update
    void Start()
    {
        maxOffset = new Vector2(Mathf.Abs(maxOffset.x), maxOffset.y);

        Vector2 leftBottom = GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.25f, 0.25f, 0));
        Vector2 rightTop = GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.75f, 0.75f, 0));

        //Vector2 leftBottom = GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0f, 0f, 0));
        //Vector2 rightTop = GetComponent<Camera>().ViewportToWorldPoint(new Vector3(1f, 1f, 0));
        cameraWorldHalfSize = new Vector2(rightTop.x - leftBottom.x, rightTop.y - leftBottom.y);
        if (followObject)
        {
            //destination = followObject.position;
            transform.position = new Vector3(followObject.position.x, followObject.position.y, transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (followObject) {
            Vector3 temp = new Vector3(destination.x, destination.y, 0) - transform.position;
            transform.position += new Vector3(temp.x / 32, temp.y / 32, 0);
            Vector3 offset = maxOffset;
            if (followObject.localScale.x > 0)
            {
                destination.x = followObject.position.x + offset.x;
                destination.y = followObject.position.y + offset.y;
            }
            else
            {
                destination.x = followObject.position.x - offset.x;
                destination.y = followObject.position.y + offset.y;
            }
            

            float clampX = Mathf.Clamp(transform.position.x, sceneArea.xMin + cameraWorldHalfSize.x, sceneArea.xMax - cameraWorldHalfSize.x);
            float clampY = Mathf.Clamp(transform.position.y, sceneArea.yMin + cameraWorldHalfSize.y, sceneArea.yMax - cameraWorldHalfSize.y);
            transform.position = new Vector3(clampX, clampY, transform.position.z);
        }


    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireCube(centerScene, sizeScene);
        Gizmos.DrawWireCube(sceneArea.center, sceneArea.size);
        
        //Vector2 cameraHalfSize = new Vector2(Camera.current.orthographicSize / 2, Camera.current.orthographicSize / 2);
        //Gizmos.DrawWireCube(transform.position, cameraWorldHalfSize);

        //Gizmos.DrawLine(new Vector3(destination.x, centerScene.y - sizeScene.y / 2), new Vector3(destination.x, centerScene.y + sizeScene.y / 2));
        //Gizmos.DrawLine(new Vector3(centerScene.x - sizeScene.x / 2, destination.y), new Vector3(centerScene.x + sizeScene.x / 2, destination.y));

    }

}
