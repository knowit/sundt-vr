using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastGun : MonoBehaviour
{
    public MouseButton fireButton;
    public Transform originOverride;

    private readonly float distance = 1000.0f;

    private bool _isEquipped = false;
    private RaygunReciever _hover;

    void Pickup() => _isEquipped = true;
    void Drop() => _isEquipped = false;

    private Ray Forward => new Ray
    {
        origin = originOverride?.position ?? transform.position,
        direction = originOverride?.forward ?? transform.forward
    };

    void Update()
    {
        if (_isEquipped)
        {
            var ray = Forward;

            if (Physics.Raycast(ray, out var hit, distance, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Collide))
            {
                var reciever = hit.collider.GetComponentInParent<RaygunReciever>();
                if (reciever != null)
                {
                    if (_hover != reciever)
                    {
                        if (_hover)
                            _hover.OnRaygunExit();
                        _hover = reciever;
                        _hover.OnRaygunEnter(hit);
                    }

                    if (Input.GetMouseButton((int)fireButton))
                    {
                        Debug.DrawRay(ray.origin, ray.direction * distance, Color.green);
                        _hover.OnRaygunShoot(hit);
                    } 
                    else
                    {
                        Debug.DrawRay(ray.origin, ray.direction * distance, Color.cyan);
                    }
                    _hover.OnRaygunHover(hit);
                    return;
                }
                else if (_hover)
                {
                    _hover.OnRaygunExit();
                    _hover = null;
                }
            }

            if (_hover)
            {
                _hover.OnRaygunExit();
                _hover = null;
            }
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
        }
    }
}
