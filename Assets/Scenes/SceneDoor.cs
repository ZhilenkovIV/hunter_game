using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDoor : MonoBehaviour
{
    public BoolValue doorIsExist;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(doorIsExist.value);
        GetComponent<TakeDamage>().dieAction += (n) =>
        {
            doorIsExist.value = false;
            Destroy(n);
        };
    }

}
