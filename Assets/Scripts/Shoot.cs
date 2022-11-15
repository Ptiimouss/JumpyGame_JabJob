using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float throwForce = 20f;
    public GameObject Projectile;
    public GameObject gunHandler;
    float cd = 0;
    RaycastHit hitInfo;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.timeScale != 0f && cd <= 0)
        {
            Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo);
            GameObject proj = Instantiate(Projectile, gunHandler.transform.position, gunHandler.transform.rotation);
            Rigidbody pj = proj.GetComponent<Rigidbody>();
            pj.transform.LookAt(hitInfo.point);
            pj.AddForce(pj.transform.forward * throwForce, ForceMode.VelocityChange);
            cd = 0.5f;
        }
        cd -= Time.deltaTime;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, hitInfo.point);
    }
}