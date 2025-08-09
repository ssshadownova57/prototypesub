using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PrototypeSubMod.MiscMonobehaviors;

public class RotateOverTime : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 initialVector;
    [SerializeField] private float timeBetweenVectorChanges;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float axisAlignmentSpeed;

    private Vector3 currentAxis;
    private Vector3 startAxis;
    private Vector3 targetAxis;
    private Vector3 poleAxis;
    private float axisChangeCountdown;
    private float currentRotationTime;
    private float currentAngle;
    
    private void Start()
    {
        targetAxis = initialVector.normalized;
        startAxis = target.eulerAngles;
        poleAxis = (targetAxis + startAxis) * 0.5f;
    }

    private void Update()
    {
        if (axisChangeCountdown < timeBetweenVectorChanges)
        {
            axisChangeCountdown += Time.deltaTime;
        }
        else
        {
            axisChangeCountdown = 0;
            startAxis = currentAxis.normalized;
            poleAxis = targetAxis.normalized;
            targetAxis = Random.onUnitSphere.normalized;
        }

        if (currentRotationTime < rotationSpeed)
        {
            currentRotationTime += Time.deltaTime;
        }
        else
        {
            currentRotationTime = 0;
        }

        currentAngle += Time.deltaTime * rotationSpeed;
        target.rotation = Quaternion.AngleAxis(currentAngle, currentAxis.normalized);
        float normalizedProgress = currentRotationTime / rotationSpeed;
        var seg1Axis = Vector3.Lerp(startAxis, poleAxis, normalizedProgress).normalized;
        var seg2Axis = Vector3.Lerp(poleAxis, targetAxis, normalizedProgress).normalized;
        var finalAxis = Vector3.Lerp(seg1Axis, seg2Axis, normalizedProgress).normalized;
        currentAxis = Vector3.Lerp(currentAxis, finalAxis, axisAlignmentSpeed * Time.deltaTime);
    }
}