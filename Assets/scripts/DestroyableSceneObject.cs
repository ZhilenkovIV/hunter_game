using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableSceneObject : MonoBehaviour
{

    public Sprite destroedSprite;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TakeDamage>().dieAction = (n) =>
        {
            n.GetComponent<SpriteRenderer>().sprite = destroedSprite;
            n.GetComponent<TakeDamage>().enabled = false;
            n.gameObject.layer = 13;
        };
    }

}
