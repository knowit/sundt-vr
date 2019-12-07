using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptionTV : MonoBehaviour
{
    public ZapperGame zapperGame;

    public GameObject videoChild;
    public GameObject gameChild;

    private ZapperGame _gameRunning;

    void OnValidate()
    {
        if (!zapperGame)
        {
            Debug.LogError($"{name} is missing required 'ZapperGame' prefab");
        }
        if (!videoChild)
        {
            Debug.LogError($"{name} is missing required 'VideoChild' referenece");
        }
        if (!gameChild)
        {
            Debug.LogError($"{name} is missing required 'GameChild' reference");
        }
    }

    void RaygunShoot(RaycastHit hit)
    {
        if (!_gameRunning)
        {
            videoChild.SetActive(false);
            gameChild.SetActive(true);

            _gameRunning = Instantiate(zapperGame, new Vector3(0, -1000, 0), Quaternion.identity);
            return;
        }
    }

    void RaygunHover(RaycastHit hit)
    {
        if (_gameRunning)
        {
            var min = hit.collider.bounds.min;
            var max = hit.collider.bounds.max;

            _gameRunning.SetReticle(new Vector2(
                Mathf.InverseLerp(min.x, max.x, hit.point.x),
                Mathf.InverseLerp(min.y, max.y, hit.point.y)));
        }
    }
}
