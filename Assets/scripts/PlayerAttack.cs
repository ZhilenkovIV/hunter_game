using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        GetComponent<SpriteRenderer>().gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if(GetComponentInParent<Animator>().get)
    }
}
