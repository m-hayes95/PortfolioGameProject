using UnityEngine;
using Unity.Cinemachine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private CinemachineCamera[] arcadeCameras;
    private readonly int currentCamPriority = 2;
    private readonly int hiddenCamPriority = 0;

    public void SwitchToArcadeCamera(int cameraIndex)
    {
        for (int i = 0; i < arcadeCameras.Length; i++)
        {
            arcadeCameras[i].Priority = i == cameraIndex ? currentCamPriority : hiddenCamPriority;
        }
    }

    public void ExitArcadeCamera()
    {
        for (int i = 0; i < arcadeCameras.Length; i++)
        {
            arcadeCameras[i].Priority = hiddenCamPriority;
        }
    }
}
