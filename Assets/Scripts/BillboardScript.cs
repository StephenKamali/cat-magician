using UnityEngine;
using System.Collections;

public class BillboardScript : MonoBehaviour
{
    private Camera m_Camera;

    void Start()
    {
        m_Camera = FindObjectOfType<Camera>();
    }

    //Orient the camera after all movement is completed this frame to avoid jittering
    void LateUpdate()
    {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up);
    }
}