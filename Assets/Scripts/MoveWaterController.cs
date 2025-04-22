using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWaterController : MonoBehaviour
{
    public Vector3 pointB;
    public float waitTime = 3.0f;
    IEnumerator Start()
    {
        var pointA = transform.position;
        while (true)
        {
            yield return StartCoroutine(MoveFireball(transform, pointA, pointB));
            yield return StartCoroutine(MoveFireball(transform, pointB, pointA));
        }
    }

    IEnumerator MoveFireball(Transform thisTransform, Vector3 startPos, Vector3 endPos)
    {
        float i = 0.0f;
        while (i < waitTime)
        {
            i += Time.deltaTime;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }
}
