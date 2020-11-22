using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuddenlyControlPlayer : MonoBehaviour
{
    private PlayerController controller;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private IEnumerator takeOver() {

        controller.CanInputHandle = false;
        for (float time = 0; time < 0.5f; time += Time.deltaTime) {
            new MoveXCommand(controller.GetComponent<Rigidbody2D>(), -controller.maxSpeed).Execute();
            yield return null;
        }
        while (!controller.IsGrounded)
        {
            yield return null;
        }
        controller.jumpCommand.Execute();
        yield return null;
        for (float time = 0; time < 0.5f; time += Time.deltaTime)
        {
            new MoveXCommand(controller.GetComponent<Rigidbody2D>(), controller.maxSpeed).Execute();
            yield return null;
        }
        while (!controller.IsGrounded) {
            yield return null;
        }
        controller.jumpCommand.Execute();
        yield return null;
        for (float time = 0; time < 0.5f; time += Time.deltaTime)
        {
            new MoveXCommand(controller.GetComponent<Rigidbody2D>(), -controller.maxSpeed).Execute();
            yield return null;
        }
        float timeWait = 0.2f;
        new MoveXCommand(controller.GetComponent<Rigidbody2D>(), controller.maxSpeed).Execute();
        yield return new WaitForSeconds(timeWait);
        new MoveXCommand(controller.GetComponent<Rigidbody2D>(), -controller.maxSpeed).Execute();
        yield return new WaitForSeconds(timeWait);
        new MoveXCommand(controller.GetComponent<Rigidbody2D>(), controller.maxSpeed).Execute();
        yield return new WaitForSeconds(timeWait);
        new MoveXCommand(controller.GetComponent<Rigidbody2D>(), -controller.maxSpeed).Execute();
        yield return new WaitForSeconds(timeWait);
        new MoveXCommand(controller.GetComponent<Rigidbody2D>(), controller.maxSpeed).Execute();
        yield return new WaitForSeconds(timeWait);
        new MoveXCommand(controller.GetComponent<Rigidbody2D>(), -controller.maxSpeed).Execute();
        yield return new WaitForSeconds(timeWait);
        new MoveXCommand(controller.GetComponent<Rigidbody2D>(), controller.maxSpeed).Execute();
        yield return new WaitForSeconds(timeWait);
        new MoveXCommand(controller.GetComponent<Rigidbody2D>(), -controller.maxSpeed).Execute();
        yield return new WaitForSeconds(timeWait);
        new MoveXCommand(controller.GetComponent<Rigidbody2D>(), controller.maxSpeed).Execute();
        yield return new WaitForSeconds(timeWait);
        new MoveXCommand(controller.GetComponent<Rigidbody2D>(), -controller.maxSpeed).Execute();
        yield return null;
        controller.CanInputHandle = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            controller = collision.GetComponent<PlayerController>();
            StartCoroutine(takeOver());
        }
    }
}
