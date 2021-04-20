using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float lerpTime;

    void Update()
    {
        Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetPos, lerpTime);
    }
}
