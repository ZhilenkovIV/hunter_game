using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
	public SceneInfo newScene;
	public int numberEnter;

    public Vector2 direction;
    private PlayerController controller;
    private Rigidbody2D playerRB;
    private Vector2 enterDistance;

    private MoveXCommand move;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerRB = controller.GetComponent<Rigidbody2D>();
        move = controller.GetComponent<MoveXCommand>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            controller.CanInputHandle = false;
            enterDistance = transform.position - controller.transform.position;
            enterDistance.x = Mathf.Sign(enterDistance.x);
            enterDistance.y = Mathf.Sign(enterDistance.y);

            move.speed = enterDistance.x * controller.maxSpeed;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (enterDistance.x == direction.x || enterDistance.y == direction.y)
            {
                //levelChanger.FadeToLevel(levelToLoad, newPlayerPosition);
                playerRB.GetComponent<InitPosition>().position.value = newScene.enterPoints[numberEnter];
                SceneManager.LoadScene(newScene.number);

                //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().CanInputHandle = true;
            }
            else
            {
                controller.CanInputHandle = true;
            }
        }
    }


    /*

    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (enterDistance.x == direction.x || enterDistance.y == direction.y)
            {
                levelChanger.FadeToLevel(levelToLoad, newPlayerPosition);
            }
            else
            {
                controller.CanInputHandle = true;
            }
        }
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
    }*/

}
