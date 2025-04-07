using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Grasshopper_GameObserver : MonoBehaviour
{
    public static int HopUnits = 0;
    [SerializeField] private TMP_Text Hop_Count;
    [SerializeField] private TMP_Text Hop_Face;
    [SerializeField] private Transform StoneValNeg4;
    [SerializeField] private Transform StoneValNeg3;
    [SerializeField] private Transform StoneValNeg2;
    [SerializeField] private Transform StoneValNeg1;
    [SerializeField] private Transform StoneVal0;
    [SerializeField] private Transform StoneVal1;
    [SerializeField] private Transform StoneVal2;
    [SerializeField] private Transform StoneVal3;
    [SerializeField] private Transform StoneVal4;
    [SerializeField] private Transform Grasshopper;
    [SerializeField] private float jumpSpeed = 3.0f;

    [SerializeField] public Local_Observer gameObserver;

    private int currentPosition = -2;
    public static bool Hop_Direction = false;

    public int replaytimes = 0;
    [SerializeField] private GameObject toolTipScreen;
    [SerializeField] private GameObject completeScreen;
    [SerializeField] public Stage_Record_Observer Stage_Complete;

    private void Start()
    {

        UpdateHopCountText();
        UpdateHopDirectionText();
        if (Stage_Complete = null)
        {
            Debug.Log("Are u kidding me?");
        }
        Debug.Log("Game Started - Replay times: " + replaytimes);
        if (Stage_Complete == null)
        {
            Stage_Complete = FindObjectOfType<Stage_Record_Observer>();
        }

        if (Stage_Complete == null)
        {
            Debug.LogError("Stage_Record_Observer not found in the scene! Please assign it manually.");
        }
    }
    public void Jump()
    {
        int targetPosition = currentPosition + (Hop_Direction ? -HopUnits : HopUnits);
        StartCoroutine(JumpAcrossStones(targetPosition));
    }

    public void CheckStageComplete()
    {
        StartCoroutine(CheckStageCompleteCoroutine());
    }

    private IEnumerator CheckStageCompleteCoroutine()
    {
        Debug.Log("Checking stage completion...");
        if (HopUnits == 5 && !Hop_Direction)
        {
            Debug.Log("Stage complete!");

            if (Stage_Complete != null)
            {
                completeScreen.SetActive(true);
                Stage_Complete.GrassHopperStageComplete();
            }
            else
            {
                Debug.LogError("Stage_Complete is null! Please assign it in the Inspector.");
            }
        }
        else
        {
            Debug.Log("Wrong answer! Bouncing back...");

            replaytimes++;  // ✅ Ensuring replay count increments properly
            yield return BounceBackToStoneNeg2();
        }
    }

    private IEnumerator JumpAcrossStones(int targetPosition)
    {
        int step = targetPosition > currentPosition ? 1 : -1;

        while (currentPosition != targetPosition)
        {
            int nextPosition = currentPosition + step;
            Transform nextStone = GetStoneByPosition(nextPosition);

            if (nextStone != null)
            {
                yield return JumpToIntermediateStone(nextStone);
                currentPosition = nextPosition;
                Debug.Log("Grasshopper jumped to position " + currentPosition);
            }
            else
            {
                if (nextPosition < -4 || nextPosition > 4)
                {
                    if (HopUnits > 1)
                    {
                        Debug.Log("Out of bounds. Returning to position -2.");
                        replaytimes++;  // ✅ Ensuring replay count increments before bouncing back
                        yield return BounceBackToStoneNeg2();
                        yield break;
                    }
                    else
                    {
                        Debug.Log("No more moves left. Checking stage completion.");
                        CheckStageComplete();
                        yield break;
                    }
                }
            }
            yield return new WaitForSeconds(0.4f);
        }

        Debug.Log("Finished hopping. Checking completion.");
        CheckStageComplete();
    }

    private IEnumerator JumpToIntermediateStone(Transform targetStone)
    {
        Vector3 startPosition = Grasshopper.position;
        Vector3 endPosition = targetStone.position;
        endPosition.y = Mathf.Max(endPosition.y, startPosition.y);
        float elapsedTime = 0f;
        float jumpHeight = 100.0f;

        while (elapsedTime < 1f)
        {
            float progress = elapsedTime / 1f;
            Vector3 position = Vector3.Lerp(startPosition, endPosition, progress);
            position.y += Mathf.Sin(progress * Mathf.PI) * jumpHeight;

            Grasshopper.position = position;
            elapsedTime += Time.deltaTime * jumpSpeed;
            yield return null;
        }

        Grasshopper.position = endPosition;
        Debug.Log($"Grasshopper moved to {endPosition}");
    }

    private IEnumerator BounceBackToStoneNeg2()
    {
        Debug.Log("Bouncing back to Stone -2...");

        Transform targetStone = StoneValNeg2;
        if (targetStone != null)
        {
            yield return JumpToIntermediateStone(targetStone);
            currentPosition = -2;
            Debug.Log("Grasshopper bounced back to position -2");
            if (Stage_Complete.GrasshopperReplayTimes == 2)
            {
                Debug.Log("SHOW TIP SCREEN PLEASE");
                Stage_Complete.TipScreen.SetActive(true);
                Stage_Complete.TipButton.SetActive(true);
            }
        }
        else
        {
            Debug.LogError("StoneValNeg2 is NULL! Make sure it's assigned in the Inspector.");
            yield break;
        }

        replaytimes++;  // ✅ Ensure replay count is incremented correctly
        Debug.Log("Replaytimes incremented: " + replaytimes);

        if (Stage_Complete != null)
        {
            Stage_Complete.GrasshopperReplayTimes++;
        }
        else
        {
            Debug.LogError("Stage_Complete is NULL! Make sure it's assigned in the Inspector.");
        }

        if (replaytimes == 2)
        {
            if (gameObserver != null)
            {
                gameObserver.ForceTipScreen();
            }
            else
            {
                Debug.LogError("gameObserver is NULL! Ensure it's assigned.");
            }
        }
    }

    private Transform GetStoneByPosition(int position)
    {
        return position switch
        {
            -4 => StoneValNeg4,
            -3 => StoneValNeg3,
            -2 => StoneValNeg2,
            -1 => StoneValNeg1,
            0 => StoneVal0,
            1 => StoneVal1,
            2 => StoneVal2,
            3 => StoneVal3,
            4 => StoneVal4,
            _ => null,
        };
    }

    public void Increment_Hops()
    {
        HopUnits += 1;
        UpdateHopCountText();
    }

    public void Decrement_Hops()
    {
        if (HopUnits > 0)
        {
            HopUnits -= 1;
            UpdateHopCountText();
        }
    }

    public void Toggle_Direction()
    {
        Hop_Direction = !Hop_Direction;
        UpdateHopDirectionText();
    }

    private void UpdateHopDirectionText()
    {
        if (Hop_Face != null)
        {
            Hop_Face.text = Hop_Direction ? "left" : "right";
        }
    }

    private void UpdateHopCountText()
    {
        if (Hop_Count != null)
        {
            Hop_Count.text = HopUnits.ToString();
        }
    }
}
