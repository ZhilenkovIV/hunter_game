using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    public Transform player;
    public Vector2 speed;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = player.position - transform.position;
        float delta = Time.deltaTime;
    }
}
