using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterface : MonoBehaviour
{
    public TakeDamage takeDamage;
    public Sprite sprite;
    public Vector2 offset;
    public Vector2 distance;
    public Vector3 scale;
    GameObject[] points;
    int oldHp;

    // Start is called before the first frame update
    void Start()
    {
        points = new GameObject[takeDamage.healthPoints];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = new GameObject("health" + i);
            points[i].transform.parent = transform;
            points[i].transform.localScale = scale;
            points[i].transform.localPosition = offset + i * distance;
            points[i].AddComponent<SpriteRenderer>();
            points[i].GetComponent<SpriteRenderer>().sprite = sprite;

        }
        oldHp = takeDamage.healthPoints;
    }

    // Update is called once per frame
    void Update()
    {
        int currentHp = takeDamage.healthPoints;
        //if (oldHp != currentHp){
        if (true) {

            for (int i = 0; i < points.Length; i++)
            {
                points[i].GetComponent<SpriteRenderer>().enabled = false;

            }

            for (int i = 0; i < currentHp; i++)
            {
                points[i].transform.localScale = scale;
                points[i].transform.localPosition = offset + i * distance ;
                points[i].GetComponent<SpriteRenderer>().enabled = true;
            }
            oldHp = currentHp;
        }
    }
}
