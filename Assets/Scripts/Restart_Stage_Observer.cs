using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart_Stage_Observer : MonoBehaviour
{

        [SerializeField] private DragnDrop_GameObserver gameObserver; 
    // Start is called before the first frame update
    public void ResetTheWorld(){
        gameObserver.DraggableReset();
        gameObserver.replaytimes = 0;
        gameObserver.completeScreen.SetActive(false);
        Debug.Log("It should work now");
    }
}
