using System.Collections;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public int maxBounce = 6;
    public Transform laserOrigin;
    public GameObject laserEnd;
    public Material power;
    public Material off;
    public LineRenderer line;
    public float laserRange;
    public GameObject mirrorPre;
    public GameObject boosterPre;
    private Vector3 spawn = new Vector3(0,0,0);
    
    private void Update()
    {
        laserRange = 200.0f;
        Vector3 currentPosition = laserOrigin.position;
        Vector3 currentDirection = laserOrigin.transform.up;

        line.positionCount = 1;

        line.SetPosition(0, currentPosition);

        for (int i = 0; i < maxBounce; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(currentPosition, currentDirection, out hit, laserRange))
            {
                if (hit.collider.CompareTag("Mirror"))
                {
                    line.positionCount = i + 2;
                    line.SetPosition(i + 1, hit.point);
                    currentDirection = Vector3.Reflect(currentDirection, hit.normal);
                    currentPosition = hit.point;
                }
                else if (hit.collider.CompareTag("Booster"))
                { 
                    line.positionCount = i + 2;
                    line.SetPosition(i + 1, hit.point);
                    
                    currentDirection = hit.collider.transform.up;
                    currentPosition = hit.collider.transform.position;
                }
                else if (hit.collider.CompareTag("LaserAccepter"))
                {
                    line.positionCount = i + 2;
                    line.SetPosition(i + 1, hit.point);
                    laserEnd.GetComponent<Renderer>().sharedMaterial = power;
                    break;
                }
                else
                {
                    line.positionCount = i + 2;
                    line.SetPosition(i + 1, hit.point);
                }
                if (!hit.collider.CompareTag("LaserAccepter"))
                {
                    laserEnd.GetComponent<Renderer>().sharedMaterial = off;
                }
            }
            else
            {
                line.positionCount = i + 2;
                line.SetPosition(i + 1, hit.point);
                break;
            }
        }
    }
    public void SpawnMirror()
    {
        Instantiate(mirrorPre, new Vector3(0,1.5f,0), Quaternion.Euler(new Vector3(90,-45,0)));
    }
    public void SpawnBooster()
    {
        Instantiate(boosterPre, new Vector3(0, 1f, 0), Quaternion.identity);
    }
}
