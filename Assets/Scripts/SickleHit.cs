using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickleHit : MonoBehaviour
{
    public GameObject sickle;
    public static bool canShoot;
    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        shoot();
    }

    private void shoot()
    {
        if (Input.GetKeyDown(KeyCode.U) && canShoot)
        {
            Instantiate(sickle, this.transform.position, this.transform.rotation);
            canShoot = false;
        }
    }
}
