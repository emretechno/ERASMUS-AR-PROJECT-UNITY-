using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float _mult = 1f;

    public float rotateSpeed = 4000.0f; // Increased rotation speed
    public float speed = 4000.0f; // Increased movement speed
    public float zoomSpeed = 1500000.0f; // Increased zoom speed

    private void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        float rotate = 0f;
        if (Input.GetKey(KeyCode.Q)) rotate = -1f;
        else if (Input.GetKey(KeyCode.E)) rotate = 1f;

        // Increase multiplier when LeftShift is held down
        _mult = Input.GetKey(KeyCode.LeftShift) ? 4f : 2f;

        // Apply rotation
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime * rotate * _mult, Space.World);

        // Apply translation
        transform.Translate(new Vector3(hor, 0, ver) * Time.deltaTime * _mult * speed, Space.Self);

        // Apply zoom
        transform.position += transform.up * zoomSpeed * Time.deltaTime * Input.GetAxis("Mouse ScrollWheel");

        // Clamp the position's y value to keep the camera within certain bounds
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y, -20f, 30f),
            transform.position.z);
    }
}
