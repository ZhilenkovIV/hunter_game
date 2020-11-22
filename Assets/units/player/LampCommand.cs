using UnityEngine;
using System.Collections;

public class LampCommand : ICommand
{
    GameObject lamp;

    public LampCommand(GameObject lampObject) {
        this.lamp = lampObject;
    }
    
    public void Execute()
    {
        lamp.SetActive(true);
    }

    public void Undo()
    {
        lamp.SetActive(false);
    }

}
