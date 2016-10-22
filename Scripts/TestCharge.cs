﻿using UnityEngine;
using System.Collections;

public class TestCharge : MonoBehaviour
{
    public int Charge { get; set; }
    public const long K = 9000000000;

    private Vector3 _direction;

    private void Start()
    {
        Charge = 1;
    }

	private void Update ()
    {
        SetDirection();
	}

    private void SetDirection()
    {
        Vector3 direction = Vector3.zero;
        double sigmaF = 0;
        for(int i = 0; i < EntityManager.Particles.Count; i++)
        {
            QParticle particle = EntityManager.Particles[i];
            double distance = Vector3.Distance(particle.transform.position, transform.position);
            double f = (particle.ElectricCharge * particle.ElectricCharge) /(distance * distance) * Mathf.Sign(particle.ElectricCharge);
            sigmaF += f;
            direction += (float)f * -1 *  (particle.transform.position - transform.position);
        }
        transform.localScale = new Vector3(0.015f, 0.015f, Mathf.Clamp(Mathf.Sqrt(direction.magnitude) / 4,0,0.3f));
        direction.Normalize();
        transform.LookAt(transform.position + new Vector3(direction.x,  direction.y,0));
    }
}
