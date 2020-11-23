using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableSceneObject : MonoBehaviour
{

    public Sprite destroedSprite;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TakeDamage>().dieAction += () =>
        {
            GetComponent<SpriteRenderer>().sprite = destroedSprite;
            GetComponent<TakeDamage>().enabled = false;
            gameObject.layer = 13;
        };
    }

}
