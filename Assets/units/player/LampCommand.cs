using UnityEngine;
using System.Collections;

public class LampCommand : MonoBehaviour, ICommand
{
    public GameObject lamp;
    
    public void Execute()
    {
        lamp.SetActive(true);
    }

    public void Undo()
    {
        lamp.SetActive(false);
    }

}
