
using UnityEngine;

//Skrypt do strzelania
//Krzysiek
public class GunShooting : MonoBehaviour
{
    public float damage = 10;
    public float range = 250f;
    public GameObject impactEffect;
    public ParticleSystem muzzleFlash;
    public Camera fpsCam;

    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    
    void Shoot()
    {
        RaycastHit hit;
        if( Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
           Enemy enemy= hit.transform.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }

    }
}
