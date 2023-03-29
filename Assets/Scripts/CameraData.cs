using Cinemachine;
using UnityEngine;

[System.Serializable]
public class CameraData
{
    public Camera camera;
    public CinemachineVirtualCamera virtualCamera;

    public void ResetCamera(){ 
        camera.transform.position = virtualCamera.transform.position;
        camera.transform.rotation = virtualCamera.transform.rotation;
        virtualCamera.Priority = 10;
    }

    public void DesativarCamera() { 
        camera.gameObject.SetActive(false);
        virtualCamera.Priority = 0;
    }

    public void AtivarCamera()
    {
        camera.gameObject.SetActive(true);
        ResetCamera();
    }
}
