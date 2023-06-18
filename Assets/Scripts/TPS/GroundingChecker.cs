using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundingChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _surfaceMask;
    [SerializeField] private float _surfaceDistance = 0.2f;

    private int _lastCheckingFrame;
    private bool _lastValue;

    public bool IsOnSurface { 
        get
        {
            if (_lastCheckingFrame != Time.frameCount)
            {
                _lastCheckingFrame = Time.frameCount;
                _lastValue = Physics.CheckSphere(transform.position, _surfaceDistance, _surfaceMask);
            }
            return _lastValue;
        }
    }
}
