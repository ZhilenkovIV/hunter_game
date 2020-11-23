using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSparcles : MonoBehaviour
{
    public DealDamage dealDamage;
    private ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        dealDamage.attackPass += (n) => particles.Play();
    }

}
