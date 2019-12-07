using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastPickup : MonoBehaviour
{
    public float pickupDistance = 5;
    public KeyCode pickupButton = KeyCode.E;

    public Transform transformOverride;
    public Transform pickupTarget;

    private int _layer = -1;

    private Pickupable _equipped;

    void Awake()
    {
        _layer = 1 << LayerMask.NameToLayer("Pickup");
    }

    void OnValidate()
    {
        if (!pickupTarget)
        {
            Debug.LogError($"{name} is missing a pickup target");
        }
    }

    private Ray Forward => new Ray
    {
        origin = transformOverride?.position ?? transform.position,
        direction = transformOverride?.forward ?? transform.forward
    };

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(Forward);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(pickupButton))
        {
            if (_equipped)
            {
                Drop();
                return;
            }

            var ray = Forward;

            if (Physics.Raycast(ray, out var hit, pickupDistance, _layer, QueryTriggerInteraction.Collide))
            {
                Debug.DrawRay(ray.origin, ray.direction * pickupDistance, Color.green);
                var pickupable = hit.collider.GetComponentInParent<Pickupable>();
                if (pickupable != null)
                {
                    Pickup(pickupable);
                }
            } 
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * pickupDistance, Color.red);
            }
        }
    }

    void Drop()
    {
        _equipped.OnDrop();
        _equipped.transform.SetParent(null, true);
        _equipped = null;
    }

    void Pickup(Pickupable pickup)
    {
        pickup.OnPickup();
        _equipped = pickup;
        _equipped.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        _equipped.transform.SetParent(pickupTarget, false);
    }
}
