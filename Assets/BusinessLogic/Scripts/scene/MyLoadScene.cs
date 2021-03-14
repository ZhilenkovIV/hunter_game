using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLoadScene : MonoBehaviour
{
    public LevelChanger levelChanger;
    public SceneInfo newScene;
	public int enterNo;
	public VectorValue position;

 	public void LoadScene(){
 		levelChanger.FadeToLevel(newScene, enterNo, position);
 	}
	
}
