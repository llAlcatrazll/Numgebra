using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enablePaintCan : MonoBehaviour
{
    // Start is called before the first frame update
    public bool PaintCan;

    [SerializeField] public GameObject paintcanText;

    public void TogglePaint()
    {
        PaintCan = !PaintCan;
        Debug.Log($"Paint Can {(PaintCan ? "Activated" : "Deactivated")}");
        if (PaintCan)
        {
            paintcanText.SetActive(true);
            Debug.Log("Paint can is enabled");
        }
        else
        {
            paintcanText.SetActive(false);
            Debug.Log("Paint can is not activated");
        }
    }

    void Start()
    {
        paintcanText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
