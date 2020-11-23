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
        GetComponent<TakeDamage>().dieAction += () =>
        {
            Debug.Log("Destroying door");
            doorIsExist.value = false;
            Destroy(gameObject);
            Debug.Log("Destroed " + doorIsExist.value);
        };
    }

}
