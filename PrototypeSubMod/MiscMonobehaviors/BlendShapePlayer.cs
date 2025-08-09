using System;
using UnityEngine;

namespace PrototypeSubMod.MiscMonobehaviors;

public class BlendShapePlayer : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer renderer;
    [SerializeField] private int shapeIndex;
    [SerializeField] private float initialValue;
    [SerializeField] private float speed;

    private float timeVal;

    private void Start()
    {
        timeVal = initialValue;
    }

    private void Update()
    {
        timeVal += Time.deltaTime * speed;
        var weight = Mathf.PingPong(timeVal, 100);

        renderer.SetBlendShapeWeight(shapeIndex, weight);
    }
}