using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject menu;

    [SerializeField]
    GameObject timer;
    CountdownTimer this_timer;

    [SerializeField]
    GameObject PositionObj;
    Change_pos pos_script;

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);

        this_timer = timer.GetComponent<CountdownTimer>();
        pos_script = PositionObj.GetComponent<Change_pos>();
    }

    public void ToMenu()
    {
        menu.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void StartApp()
    {
        //Set all the appropriate things and then run
        //Timer - Done
        //Objects
        //Points

        //-----Timer set----
        this_timer.SetTimer(10.0f);
        this_timer.ResetTimer();
        //-----------------

        pos_script.ResetBox();
        pos_script.ResetOtherObjects();

        menu.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
