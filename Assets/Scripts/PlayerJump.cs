using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private RectTransform rope;
    [SerializeField] private RectTransform landingSpot;
    [SerializeField] private float jumpDuration = 1.5f;

    public void StartJump()
    {
        StartCoroutine(JumpSequence());
    }

    private IEnumerator JumpSequence()
    {
        float halfDuration = jumpDuration / 2f;
        yield return MoveToTarget(player.transform, rope.position, halfDuration);
        yield return MoveToTarget(player.transform, landingSpot.position, halfDuration);
    }

    private IEnumerator MoveToTarget(Transform obj, Vector3 targetPos, float duration)
    {
        Vector3 startPos = obj.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            obj.position = Vector3.Lerp(startPos, targetPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.position = targetPos;
    }
}
