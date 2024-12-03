using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
   
    public Transform lasersource;
    private void Start()
    {
        Reflect(lasersource.position, lasersource.transform.forward);
    }
    void Reflect(Vector3 startposition, Vector3 direction)
    {
        Ray ray = new Ray(startposition, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
            Debug.DrawRay(hit.point, reflectDir * 10, Color.red);
            Reflect(hit.point, reflectDir);
        }
    }
}
