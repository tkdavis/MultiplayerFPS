using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float damage = 10.0f;
    public float range = 100.0f;
    public GameObject mainCam;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            PawnController pawn = hit.transform.parent.GetComponent<PawnController>();
            if (pawn != null)
            {
                pawn.TakeDamage(damage);
            }
        }
    }
}
