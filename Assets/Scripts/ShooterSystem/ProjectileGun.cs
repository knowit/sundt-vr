using System.Collections;
using UnityEngine;

public enum MouseButton
{
    Left = 0,
    Right = 1
}

public class ProjectileGun : MonoBehaviour
{
    public Projectile projectile;
    public Transform spawnOverride;
    public double fireSpeed = 0.2f;
    public MouseButton fireButton;

    private bool _isEquipped = false;
    private double _spawnTime;

    void Pickup() => _isEquipped = true;
    void Drop() => _isEquipped = false;

    void Start()
    {
        _spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isEquipped && Input.GetMouseButton((int)fireButton) && Time.time-_spawnTime > fireSpeed)
        {
            Instantiate(projectile,
                spawnOverride?.position ?? transform.position, 
                spawnOverride?.rotation ?? transform.rotation);
            _spawnTime = Time.time;
        }
    }
}
