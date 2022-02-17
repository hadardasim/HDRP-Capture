using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    // number of frames to move from one side to the other
    public int moveFrameCounter = 4;
    public Vector3 moveOffset = new Vector3(4, 0, 0);

    Vector3 startPosition;
    
    void Start()
    {
        startPosition = transform.position;
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
        
    }
}
