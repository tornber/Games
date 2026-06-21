using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float reloadTime = 1f;
    public float shootRate = 0.15f;
    public int maxSize = 30;
    public bool isGlock = false;

    public AudioClip shootingSFX;

    public GameObject bullet;
    public Transform bulletspawnPlace;
    public GameObject weaponFlash;
    public GameObject droppedWeapon;

    public float recoilDistance = 0.1f;
    public float recoilSpeed = 15f;

    private int currentAmmo;
    private bool isReloading = false;
    private float nextTimeToFire = 0f;

    private Quaternion initialRotation;
    private Vector3 initialPosition;
    private Vector3 reloadRotationOffset = new Vector3(66, 50, 50);

    void Start()
    {
        currentAmmo = maxSize;
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
        UIManager.Instance.ammoText.text = currentAmmo.ToString();

    }

    public void Shoot()
    {
        if (isReloading) return;
        if (Time.time < nextTimeToFire) return;
         
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        Quaternion adjustRotation = bulletspawnPlace.rotation * Quaternion.Euler(0f,0f,0f);

        Instantiate(bullet, bulletspawnPlace.position, adjustRotation);
        currentAmmo--;
        UIManager.Instance.ammoText.text = currentAmmo.ToString();

        AudioManager.Instance.PlaySFX(shootingSFX,0.25f);

        nextTimeToFire = Time.time + shootRate;

        StopCoroutine(nameof(Recoil));
        StartCoroutine(nameof(Recoil));

    }

    IEnumerator Reload()
    {
        isReloading = true;

        if(isGlock)
        {
            UIManager.Instance.ammoText.text = "0 / 12";
        } else
        {
            UIManager.Instance.ammoText.text = "0 / 30";
        }
        Quaternion targetRotation = Quaternion.Euler(initialRotation.eulerAngles + reloadRotationOffset);
        float halfReload = reloadTime / 2;
        float t = 0f;

        while(t < halfReload)
        {
            t += Time.deltaTime;
            transform.localRotation = Quaternion.Slerp(initialRotation,targetRotation, t / halfReload);
            yield return null;  
        }

        t = 0f;

        while(t < halfReload)
        {
            t += Time.deltaTime;
            transform.localRotation = Quaternion.Slerp(targetRotation,initialRotation, t / halfReload);
            yield return null;
        }

        currentAmmo = maxSize;
        UIManager.Instance.ammoText.text = currentAmmo.ToString();
        isReloading = false;
    }

    public void TryReload()
    {
        if (isReloading) return;
        if (currentAmmo == maxSize) return;

        StartCoroutine(Reload());
    }


    private IEnumerator Recoil()
    {
        Vector3 recoilTarget = initialPosition + new Vector3(recoilDistance, 0, 0);
        float t = 0f;

        while(t < 1f )
        {
            t += Time.deltaTime * recoilSpeed;
            transform.localPosition = Vector3.Lerp(initialPosition, recoilTarget, t);
            yield return null;
        }

        t = 0f;

        while( t < 1f )
        {
            t += Time.deltaTime * recoilSpeed;
            transform.localPosition = Vector3.Lerp(recoilTarget, initialPosition, t);
            yield return null;
        }

        transform.localPosition = initialPosition; 

    }


    public void Drop()
    {
        UIManager.Instance.ammoText.text = "";
        Instantiate(droppedWeapon, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
