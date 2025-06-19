using UnityEngine;

public class BillbordUI : MonoBehaviour
{
    private GameObject cam;

    private void Awake()
    {
        InitCam(Camera.main.gameObject);
    }

    public void InitCam(GameObject camObj)
    {
        cam = camObj;
    }

    void LateUpdate()
    {
        if (cam == null) return;

        Vector3 targetPos = transform.position + cam.transform.rotation * Vector3.forward;
        Vector3 upDir = cam.transform.rotation * Vector3.up;

        transform.LookAt(targetPos, upDir);
    }
}
