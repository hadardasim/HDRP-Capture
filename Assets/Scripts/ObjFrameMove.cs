using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjFrameMove : MonoBehaviour
{
    Vector3 startPosition;
    // number of frames to move from one side to the other
    public int moveFrameCounter = 5;
    public Vector3 moveOffset = new Vector3(4, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        int _step = Time.frameCount % (moveFrameCounter * 2);
        float _ratio = _step / (float)moveFrameCounter;
        if (_step > moveFrameCounter)
            _ratio = 2 - _ratio;
        transform.position = startPosition + moveOffset * _ratio;
    }
}
