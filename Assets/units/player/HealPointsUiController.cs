using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPointsUiController : MonoBehaviour
{
    private TakeDamage player;
    public int hpObserver;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<TakeDamage>();
        player.damageAction += (n) => { if (player.hp == hpObserver) gameObject.SetActive(false); };
    }

}
