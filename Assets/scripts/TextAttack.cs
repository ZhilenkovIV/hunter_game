using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAttack : MonoBehaviour
{
    public GameObject text;
    public GameObject textLamp;
    // Start is called before the first frame update
    void Start()
    {
        PickUpEvent.Action += (s) => {
            if (s == "lamp") {
                textLamp?.SetActive(true);
                Invoke("disableTextLamp", 3f);
            }
        };
    }

    private void disableText()
    {
        Destroy(text);
    }

    private void disableTextLamp()
    {
        Destroy(textLamp);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9) {
            text?.SetActive(true);
            Invoke("disableText", 3f);
        }
    }
}
