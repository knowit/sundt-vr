using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaygunReciever : MonoBehaviour
{
    public void OnRaygunShoot(RaycastHit hit) => BroadcastMessage("RaygunShoot", hit, SendMessageOptions.DontRequireReceiver);

    public void OnRaygunEnter(RaycastHit hit) => BroadcastMessage("RaygunEnter", hit, SendMessageOptions.DontRequireReceiver);

    public void OnRaygunHover(RaycastHit hit) => BroadcastMessage("RaygunHover", hit, SendMessageOptions.DontRequireReceiver);

    public void OnRaygunExit() => BroadcastMessage("RaygunExit", SendMessageOptions.DontRequireReceiver);
}
