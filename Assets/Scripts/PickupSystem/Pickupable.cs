using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public Collider PickupCollider;

    private bool _isKinematic = false;

    void Awake()
    {
        _isKinematic = GetComponent<Rigidbody>()?.isKinematic ?? false;
    }

    void OnValidate ()
    {
        if (!PickupCollider)
        {
            Debug.LogError($"{name} is pickupable but has no pickup collider");
            return;
        }

        if (!PickupCollider.isTrigger)
        {
            Debug.LogError($"{name} is pickupable but its pickup collider is not a trigger");
        }

        if (PickupCollider.gameObject.layer != LayerMask.NameToLayer("Pickup"))
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
