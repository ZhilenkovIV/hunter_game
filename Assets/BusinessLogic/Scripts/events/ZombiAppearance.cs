using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiAppearance : MonoBehaviour
{
    public GameObject prefab;
    public float period;
    public float currentTime = 0;

    public Vector2 halfsize;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > period) {
            GameObject enemy = Instantiate(prefab);
            Vector2 position = Random.insideUnitCircle * halfsize;

            enemy.transform.position = new Vector3(position.x, position.y, 0) + transform.position;            

            currentTime = 0;
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, halfsize * 2);
    }
}
