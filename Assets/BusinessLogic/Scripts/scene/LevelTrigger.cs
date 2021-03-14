using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    public LevelChanger levelChanger;
    public SceneInfo newScene;
	public int enterNo;

    public Vector2 direction;
    private Vector2 enterDistance;

    private MoveXCommand move;
    private PlayerController controller;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            controller = collider.GetComponent<PlayerController>();
            move = collider.GetComponent<MoveXCommand>();
            controller.CanInputHandle = false;
            enterDistance = transform.position - controller.transform.position;
            enterDistance.x = Mathf.Sign(enterDistance.x);
            enterDistance.y = Mathf.Sign(enterDistance.y);

            move.speed = enterDistance.x * controller.maxSpeed;
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            if (direction.x != 0)
            {
                move.Execute();
            }
            if (direction.y > 0) {
                controller.jumpCommand.Execute();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            if (enterDistance.x == direction.x || enterDistance.y == direction.y)
            {
                levelChanger.FadeToLevel(newScene, enterNo, collider.GetComponent<InitPosition>().position);
            }
            else
            {
                controller.CanInputHandle = true;
            }
        }
    }
}
