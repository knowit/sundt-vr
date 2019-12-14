using UnityEngine;

public class Pickupable : OVRGrabbable
{
    public Collider pickupCollider;

    private bool _isKinematic = false;


    void Awake()
    {
        _isKinematic = GetComponent<Rigidbody>()?.isKinematic ?? false;
        if (m_grabPoints.Length == 0)
        {
            // Create a default grab point
            m_grabPoints = GetComponents<Collider>();
        }
    }

    override public void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
        OnPickup();
    }

    /// <summary>
    /// Notifies the object that it has been released.
    /// </summary>
    override public void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);
        OnDrop();
    }

    void OnValidate ()
    {
        if (!pickupCollider)
        {
            Debug.LogError($"{name} is pickupable but has no pickup collider");
            return;
        }

        if (!pickupCollider.isTrigger)
        {
            Debug.LogError($"{name} is pickupable but its pickup collider is not a trigger");
        }

        if (pickupCollider.gameObject.layer != LayerMask.NameToLayer("Pickup"))
        {
            Debug.LogError($"{name} is pickupable but its pickup collider is not on the pickup layer");
        }
    }

    public void OnPickup()
    {
        if (TryGetComponent<Rigidbody>(out var rigidbody))
        {
            rigidbody.isKinematic = true;
        }
        BroadcastMessage("Pickup", SendMessageOptions.DontRequireReceiver);
    }

    public void OnDrop()
    {
        if (TryGetComponent<Rigidbody>(out var rigidbody))
        {
            rigidbody.isKinematic = _isKinematic;
        }
        BroadcastMessage("Drop", SendMessageOptions.DontRequireReceiver);
    }
}
