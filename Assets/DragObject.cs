using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 offset;
    public float rotationSpeed = 100f;

    void Start()
    {
        mainCamera = Camera.main;
    }
    void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPos();
        isDragging = true;
    }
    void OnMouseUp()
    {
        isDragging = false;
    }
    void Update()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPos() + offset;
        }
        float rotateInput = Input.GetAxis("Horizontal");

        if (isDragging == true && this.tag == "BoosterObject")
        {
            transform.Rotate(Vector3.up, rotateInput * rotationSpeed * Time.deltaTime);
        }
        if (isDragging == true && this.tag == "MirrorObject")
        {
            transform.Rotate(Vector3.forward, rotateInput * rotationSpeed * Time.deltaTime);
        }


    }
    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCamera.transform.position.y;
        return mainCamera.ScreenToWorldPoint(mousePos);
    }
}
