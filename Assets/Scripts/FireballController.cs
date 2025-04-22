using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    private Vector3 pointB;
    public float yMax;
    public float waitTime = 3.0f;
    IEnumerator Start()
    {
        var pointA = transform.position;
        pointB = pointA + new Vector3(0, yMax, 0);
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
            if(startPos.y > endPos.y)
            {
                thisTransform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else
            {
                thisTransform.rotation = Quaternion.Euler(0, 0, 270);
            }
                yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponentInParent<PlayerMovement>().Die();
        }
    }

}
