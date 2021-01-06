using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterface : MonoBehaviour
{
    public Transform player;
    public GameObject hp1;
    public GameObject hp2;
    public GameObject hp3;

    // Start is called before the first frame update
    void Start()
    {
        //TakeDamageModel takeDamage = player.GetComponent<TakeDamageModel>();
        //takeDamage.damageAction += (n) =>
        //{
        //    if (takeDamage.hp == 2)
        //        hp3.SetActive(false);
        //    else if (takeDamage.hp == 1)
        //        hp2.SetActive(false);
        //    else
        //        hp1.SetActive(false);
        //};
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
