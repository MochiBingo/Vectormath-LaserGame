using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public int maxBounce;
    public Transform laserOrigin;
    private void OnDrawGizmos()
    {
        Vector3 currentPosition = laserOrigin.position;
        Vector3 currentDirection = laserOrigin.transform.up;

        for (int i = 0; i < maxBounce; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(currentPosition, currentDirection, out hit, 200.0f) && hit.collider.CompareTag("Mirror"))
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(currentPosition, hit.point);
                currentDirection = Vector3.Reflect(currentDirection, hit.normal);
                currentPosition = hit.point;
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(currentPosition, hit.point);
            }

        }
    }
}
