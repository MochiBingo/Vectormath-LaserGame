using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public int maxBounce = 5;
    public Transform laserOrigin;
    public GameObject laserEnd;
    public Material power;
    public Material off;
    public LineRenderer line;
    private void OnDrawGizmos()
    {
        Vector3 currentPosition = laserOrigin.position;
        Vector3 currentDirection = laserOrigin.transform.up;
        line.SetPosition(0, currentPosition);

        for (int i = 0; i < maxBounce; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(currentPosition, currentDirection, out hit, 200.0f) && hit.collider.CompareTag("Mirror"))
            {
                
                line.SetPosition(i, hit.point);
                currentDirection = Vector3.Reflect(currentDirection, hit.normal);
                currentPosition = hit.point;
                if (Physics.Raycast(currentPosition, currentDirection, out hit, 200.0f) && hit.collider.CompareTag("LaserAccepter"))
                {
                    line.SetPosition(i+1, hit.point);
                }
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(currentPosition, hit.point);
            }
            if (Physics.Raycast(currentPosition, currentDirection, out hit, 200.0f) && hit.collider.CompareTag("LaserAccepter"))
            {
                laserEnd.GetComponent<Renderer>().material = power;
            }
            else
            {
                laserEnd.GetComponent<Renderer>().material = off;
            }
        }
    }
}
