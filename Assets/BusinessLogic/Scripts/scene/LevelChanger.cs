using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private Animator anim;
    private int sceneNo;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void FadeToLevel(SceneInfo info, int enterNo, VectorValue position)
    {
        position.value = info.enterPoints[enterNo];
        sceneNo = info.number;
        anim.SetTrigger("Fade");
    }

    public void OnFaidComplete()
    {
        SceneManager.LoadScene(sceneNo);
    }

}
