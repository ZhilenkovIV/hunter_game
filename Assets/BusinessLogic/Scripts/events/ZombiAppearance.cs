using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiAppearance : MonoBehaviour
{
    public GameObject prefab;
    public GameObject particleObject;
    public float period;
    public float currentTime = 0;

    public Vector2 halfsize;

    private ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        particles = particleObject.GetComponent<ParticleSystem>();
    }

    IEnumerator Appearance(){

        Vector2 position = Random.insideUnitCircle * halfsize;
        particles.transform.localPosition = position;
        particles.Play();
        yield return new WaitForSeconds(particles.main.duration);

        GameObject enemy = Instantiate(prefab);

        enemy.transform.position = new Vector3(position.x, position.y, 0) + transform.position;

        
    }


    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > period) {
            StartCoroutine(Appearance());
            currentTime = 0;
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, halfsize * 2);
    }
}
