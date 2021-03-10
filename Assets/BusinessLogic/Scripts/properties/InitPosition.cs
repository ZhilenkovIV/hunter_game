using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPosition : MonoBehaviour
{
	public VectorValue position;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().position = position.value;
    }

}
