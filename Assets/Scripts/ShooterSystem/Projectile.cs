using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public double maxLifetime = 10.0;
    public float force = 15.0f;

    private double _spawned;

    // Start is called before the first frame update
    void Start()
    {
        _spawned = Time.time;

        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(transform.forward * force, ForceMode.VelocityChange);
    }

    void FixedUpdate()
    {
        if (Time.time-_spawned > maxLifetime
            || GetComponent<Rigidbody>().IsSleeping())
        {
            Destroy(gameObject);
        }
    }
}
