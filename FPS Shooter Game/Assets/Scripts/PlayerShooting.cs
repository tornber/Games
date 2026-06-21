using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public Gun gun;
    public Transform gunHolder;
    private bool isHoldingShooting = false;


    void OnShoot()
    {
        isHoldingShooting = true;
    }

    void OnShootRelease()
    {
        isHoldingShooting = false;
    }


    void OnReload()
    {
        if (gun != null)
        {
            gun.TryReload();
        }
    }

    public void OnDrop()
    {
        if (gun != null)
        {
            gun.Drop();
            gun = null;
        }
    }


    void Start()
    {
                
    }

    void Update()
    {
        if(isHoldingShooting && gun != null)
        {
            gun.Shoot();
        }
    }
}
