using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Collections;

public class MoveAliens : MonoBehaviour
{
    public List<Transform> targetTransform;
    public float speed = 5f;
    public float stayDuration = 2f;

    private int currentTargetIndex = 0;
    private bool isMoving = true;

    private void Start()
    {
        StartCoroutine(MoveTargets());
    }

    IEnumerator MoveTargets()
    {
        while (true)
        {
            if (targetTransform.Count == 0) yield break;

            Transform target = targetTransform[currentTargetIndex];

            while (Vector3.Distance(transform.position, target.position) > 0.1f)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, target.position, step);
                yield return null;
            }
            yield return new WaitForSeconds(stayDuration);

            currentTargetIndex = (currentTargetIndex + 1) % targetTransform.Count;

        }
    }
}
