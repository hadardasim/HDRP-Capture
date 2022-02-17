using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Simulation;
using UnityEngine.Experimental.Rendering;
using System.IO;

public class CameraHandler : MonoBehaviour
{
    // number of frames to move from one side to the other
    public int moveFrameCounter = 4;
    public Vector3 moveOffset = new Vector3(4, 0, 0);

    Vector3 startPosition;
    string baseDir;
    Camera captureCamera;


    void Start()
    {
        baseDir = Manager.Instance.GetDirectoryFor(DataCapturePaths.ScreenCapture);
        startPosition = transform.position;
        captureCamera = GetComponent<Camera>();
    }

    
    void Update()
    {
        int _step = Time.frameCount % (moveFrameCounter * 2);
        float _ratio = _step / (float)moveFrameCounter;
        if (_step > moveFrameCounter)
            _ratio = 2 - _ratio;
        transform.position = startPosition + moveOffset * _ratio;
    }

    private void LateUpdate()
    {
        if (Time.frameCount < 10 || Time.frameCount > 20)
            return;

        string colorPath = Path.Combine(baseDir, $"rgb_{Time.frameCount}.png");
        string depthPath = Path.Combine(baseDir, $"depth_{Time.frameCount}.exr");

        CaptureCamera.CaptureColorAndDepthToFile(captureCamera, GraphicsFormat.B8G8R8A8_SRGB, colorPath, CaptureImageEncoder.ImageFormat.Png,
            GraphicsFormat.R32_SFloat, depthPath, CaptureImageEncoder.ImageFormat.Exr);
    }
}
