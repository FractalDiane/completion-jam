using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMover : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float speed = 0.08f;

    public void Initialize()
    {
        transform.position = startPoint.position;
    }

    public IEnumerator Move()
    {
        //float moveSpeed = speed/Vector2.Distance(endPoint.position, startPoint.position);
        float progress = 0;
        while(progress < 1)
        {
            progress += speed * 60 * Time.unscaledDeltaTime;
            transform.position = Vector3.Lerp(startPoint.position, endPoint.position, progress);
            yield return null;
        }
        transform.position = endPoint.position;
    }
}
