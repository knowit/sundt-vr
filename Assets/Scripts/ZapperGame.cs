using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapperGame : MonoBehaviour
{
    public Transform reticulePrefab;
    public Camera camera;

    private Transform _reticule;

    // Start is called before the first frame update
    void Start()
    {
        _reticule = Instantiate(reticulePrefab, transform);
        _reticule.localPosition = new Vector3(0.0f, 0.0f, 4.0f);
    }
    
    public void SetReticle(Vector2 position)
    {
        if (!_reticule)
            return;

        float h = camera.orthographicSize * 2.0f;
        float w = h * camera.aspect;

        _reticule.localPosition = new Vector3(
            Mathf.Lerp(-w/2,w/2, position.x),
            Mathf.Lerp(-h/2,h/2, position.y), 4.0f);
    }
}
