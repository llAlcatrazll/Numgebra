using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExtraGrasshopper_Observer : MonoBehaviour
{
    public static int HopUnits = 0;

    public static int firstFlower = 0;
    public static int secondFlower = 0;
    public static int thirdFlower = 0;
    [SerializeField] private TMP_Text Hop_Count;
    [SerializeField] private Transform StoneValNeg5;
    [SerializeField] private Transform StoneValNeg4;
    [SerializeField] private Transform StoneValNeg3;
    [SerializeField] private Transform StoneValNeg2;
    [SerializeField] private Transform StoneValNeg1;
    [SerializeField] private Transform StoneVal0;
    [SerializeField] private Transform StoneVal1;
    [SerializeField] private Transform StoneVal2;
    [SerializeField] private Transform StoneVal3;
    [SerializeField] private Transform StoneVal4;
    [SerializeField] private Transform StoneVal5;
    [SerializeField] private Transform Grasshopper;
    [SerializeField] private float jumpSpeed = 3.0f;

    private int currentPosition = 0;
    private int sequenceIndex = 0;
    private readonly int[] correctSequence = { -2, -4, 3 };
    [SerializeField] private GameObject toolTipScreen;
    [SerializeField] private GameObject completeScreen;
    [SerializeField] private GameObject checkmarkNeg2;
    [SerializeField] private GameObject checkmarkNeg4;
    [SerializeField] private GameObject checkmark3;
    [SerializeField] private Stage_Record_Observer Stage_Complete;

    private void Start()
    {
        UpdateHopCountText();
    }

    public void Jump()
    {
        int targetPosition = HopUnits;
        StartCoroutine(JumpAcrossStones(targetPosition));
    }

    private IEnumerator JumpAcrossStones(int targetPosition)
    {
        Transform targetStone = GetStoneByPosition(targetPosition);
        if (targetStone != null)
        {
            yield return JumpToIntermediateStone(targetStone);
            currentPosition = targetPosition;
            CheckSequence(targetPosition);
        }
    }

    private void CheckSequence(int position)
    {
        if (sequenceIndex < correctSequence.Length && position == correctSequence[sequenceIndex])
        {
            EnableCheckmark(sequenceIndex);
            sequenceIndex++;

            if (sequenceIndex == correctSequence.Length)
            {
                completeScreen.SetActive(true);
                Stage_Complete.GrassHopperStageComplete();
            }
        }
        else
        {
            ResetGame();
        }
    }

    private void EnableCheckmark(int index)
    {
        if (index == 0)
        {
            checkmarkNeg2.SetActive(true);
            softReset();
        }

        else if (index == 1)
        {
            checkmarkNeg4.SetActive(true);
            softReset();
        }

        else if (index == 2) checkmark3.SetActive(true);
    }
    public void softReset()
    {
        StartCoroutine(JumpToIntermediateStone(StoneVal0));
        HopUnits = 0;
        UpdateHopCountText();
        currentPosition = 0;
    }

    private void ResetGame()
    {
        sequenceIndex = 0;
        checkmarkNeg2.SetActive(false);
        checkmarkNeg4.SetActive(false);
        checkmark3.SetActive(false);
        toolTipScreen.SetActive(true);
        StartCoroutine(JumpToIntermediateStone(StoneVal0));
        HopUnits = 0;
        UpdateHopCountText();
        currentPosition = 0;
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
    }

    private Transform GetStoneByPosition(int position)
    {
        return position switch
        {
            -5 => StoneValNeg5,
            -4 => StoneValNeg4,
            -3 => StoneValNeg3,
            -2 => StoneValNeg2,
            -1 => StoneValNeg1,
            0 => StoneVal0,
            1 => StoneVal1,
            2 => StoneVal2,
            3 => StoneVal3,
            4 => StoneVal4,
            5 => StoneVal5,
            _ => null,
        };
    }

    public void Increment_Hops()
    {
        if (HopUnits < 5)
        {
            HopUnits += 1;
            UpdateHopCountText();
        }
    }

    public void Decrement_Hops()
    {
        if (HopUnits > -5)
        {
            HopUnits -= 1;
            UpdateHopCountText();
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
