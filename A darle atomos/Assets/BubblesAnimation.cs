using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblesAnimation : MonoBehaviour
{
    public GameObject bubblePrefab;
    public AnimationClip animationClip;
    public float interval = 1.0f;
    private float animationDuration;

    private void Start()
    {
        animationDuration = animationClip.length;
        print(transform.position);
        StartCoroutine(AnimationCoroutine());
    }

    private IEnumerator AnimationCoroutine()
    {
        while (true)
        {
            GameObject bubble = Instantiate(bubblePrefab, transform);
            StartCoroutine(DestroyBubble(bubble));
            // yield return new WaitForSeconds(interval);
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

    private IEnumerator DestroyBubble(GameObject bubble)
    {
        yield return new WaitForSeconds(animationDuration);
        Destroy(bubble);
    }
}
