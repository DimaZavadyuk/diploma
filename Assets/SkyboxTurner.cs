using UnityEngine;

public class SkyboxTurner : MonoBehaviour
{
    [SerializeField] private Camera[] _cameras;

    public void TurnOnSkybox()
    {
        foreach (var camera in _cameras)
        {
            camera.clearFlags = CameraClearFlags.Skybox;
        }
    }
}