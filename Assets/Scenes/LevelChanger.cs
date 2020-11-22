using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private Animator anim;
    private PlayerController controller;
    private int levelToLoad;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void FadeToLevel(int levelToLoad, Vector3 newPlayerPosition)
    {
        controller.pos.initialValue = newPlayerPosition;
        anim.SetTrigger("Fade");
        this.levelToLoad = levelToLoad;
    }

    public void OnFaidComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

}
