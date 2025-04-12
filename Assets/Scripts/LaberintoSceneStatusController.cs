using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaberintoSceneStatusController : MonoBehaviour
{
    public int locksOpened;
    public bool allLocksOpenend = false;
    public GameObject estatua, tesoro;
    public float waitTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        locksOpened = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (locksOpened == 4 && !allLocksOpenend)
        {
            allLocksOpenend = true;
            StartCoroutine(MoveObject(estatua.transform, estatua.transform.position, estatua.transform.position + new Vector3(0, 0, 30)));
            StartCoroutine(MoveObject(tesoro.transform, tesoro.transform.position, tesoro.transform.position + new Vector3(0, 20, 0)));
        }
    }


    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos)
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
