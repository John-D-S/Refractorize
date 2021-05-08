using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneScroller : MonoBehaviour
{
    [SerializeField]
    float scrollTime = 0.1f;
    [SerializeField]
    Vector2 startPosition;
    [SerializeField]
    Vector2 endPosition;

    private float progress = 0;

    // Update is called once per frame
    void Update()
    {
        if (progress < 1)
        {
            progress += Time.deltaTime / scrollTime;
            gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, progress);
        }
        else
        {
            progress = 1;
            gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, progress);
        }
    }
}
