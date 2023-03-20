using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject menu;

    [SerializeField]
    GameObject timer;
    CountdownTimer this_timer;

    [SerializeField]
    GameObject PositionObj;
    Change_pos pos_script;

    [SerializeField]
    GameObject GameManager;
    PointSystem points_;

    [SerializeField]
    Text textElement;

    // Start is called before the first frame update
    void Start()
    {
        //menu.SetActive(true);

        this_timer = timer.GetComponent<CountdownTimer>();
        pos_script = PositionObj.GetComponent<Change_pos>();
        points_ = GameManager.GetComponent<PointSystem>();
        ToMenu();

    }

    public void ToMenu()
    {
        //Calculate the average times
        //Set the text to that

        float newAverage = 0.0f;

        if(pos_script.Timings.Count !=0)
        {
            //Get the mean
            for (int j = 0; j < pos_script.Timings.Count; j++)
            {
                newAverage += pos_script.Timings[j];
            }

            newAverage = newAverage / pos_script.Timings.Count;
        }

        //Give it to the text output
        textElement.text = newAverage.ToString();

        menu.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void StartApp()
    {
        ResetVariables();
        menu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void ResetVariables()
    {
        //Set all the appropriate things and then run
        //Timer - Done
        //Objects - Done
        //Points

        //-----Timer set----
        this_timer.SetTimer(10.0f/*100.0f*/);
        this_timer.ResetTimer();
        //-----------------

        //-----Positions-----
        pos_script.ResetBox();
        pos_script.ResetOtherObjects();
        pos_script.ResetCollisions();
        //-------------------

        float newAverage = 0;

        textElement.text = newAverage.ToString();

        points_.ResetValues();
    }
}
