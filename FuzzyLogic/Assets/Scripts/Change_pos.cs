using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Change_pos : MonoBehaviour
{
    [SerializeField]
    GameObject box_obj;

    [SerializeField]
    private List<GameObject> obstacle_array;

    [SerializeField]
    GameObject GoalObject;

    [SerializeField]
    GameObject GameManager;

    PointSystem points_;

    [SerializeField]
    GameObject timer;
    CountdownTimer time_;

    private List<GameObject> array_obj;

    private float currentTime = 0;
    private float StartingTime = 15;
    private bool check_time;

    private float num_of_cols = 0.0f;
    private bool had_collision = false;

    [SerializeField]
    Text CollisionText;

    //private List<int> array_obj_space;

    //int Segment;

    bool run_positioning;

    //bool Segment_1_taken;
    //bool Segment_2_taken;
    //bool Segment_3_taken;
    //bool Segment_4_taken;

    FuzzyBox fuzzy_box;

    public List<float> Timings;
    private float current_sess_time;
    private float starting_sess_time;
    private bool StartTiming;

    //Need to find a way to change positions of all the objects once the box has got close enough to the goal
    // The obstacles should change positions
    // The goal should change position based randomly from the centre
    // Can divide up from the centre into quadrants
    // The player should reset to the centre - if its between a certain area or if it stops for a while - DONE

    // Start is called before the first frame update
    void Start()
    {

        currentTime = StartingTime;
        check_time = false;

        array_obj = new List<GameObject>();

        fuzzy_box = box_obj.GetComponent<FuzzyBox>();

        points_ = GameManager.GetComponent<PointSystem>();

        time_ = timer.GetComponent<CountdownTimer>();

        current_sess_time = 0;
        starting_sess_time = 0;
        StartTiming = true;

        //Segment = -1;

        run_positioning = false;
        //Segment_1_taken = false;
        //Segment_2_taken = false;
        //Segment_3_taken = false;
        //Segment_4_taken = false;

        array_obj.Add(GoalObject);
        array_obj.Add(obstacle_array[0]);
        //array_obj.Add(obstacle_array[1]);

        //array_obj_space.Add(-1);
        //array_obj_space.Add(-1);
        //array_obj_space.Add(-1);

        //for(int i=0; i< obstacle_array.Count;i++)
        //{
        //    array_obj.Add(obstacle_array[i]);
        //}
    }

    // Update is called once per frame
    void Update()
    {

        //Need a timer for 10 to 30 seconds to check if it hasnt reached the goal reset the objects - DONE
        //Need to time how long it takes to reach the goal could take in an average 
        //count the number of collisions with obstacle - DONE

        //Time Logic

        //Check time 
        //if correct amount of time hasnt passed
        //keep going
        //if correct amount of time has passed
        //call the reset function
        //However if it does hit it reset the timer

        //How long it takes logic

        //Time how long it takes to get to the destination
        //Initialise that array to have zero in zero
        //When we access it for the first time and there is zero in zero get rid of it and put the new value in its place
        //Save that time into an array
        //get the avarege of that array
        //Quick sorting algorithm
        //Smallets to biggest than get average
        //or whichever way is better to get the average
        //Display it

        //Default it to zero
        //Or if its empty just tell it to say zero

        //On reset empty it

        //Counting collisions logic

        //Initialise count to zero and then 
        //Simply increase it every time we make a collision
        //can only count for once per session though 
        //have a boolean to keep track of that
        //when the object positioning gets reset 
        //reset the boolean

        CheckTimings();


        if (Vector3.Distance(box_obj.transform.position, obstacle_array[0].transform.position) < 3)
        {
            //Deduct points
            points_.MinusPoints();
            if(had_collision == false)
            {
                num_of_cols++;
                had_collision = true;
            }
        }

        if (Vector3.Distance(box_obj.transform.position, GoalObject.transform.position) < 6.125f)
        {
            ResetBox();
        }

        if (check_time == true)
        {
            currentTime -= 1 * Time.deltaTime;
            if(currentTime <= 0.0f)
            {
                ResetBox();
            }
        }

        //Loop for every obstacle and goal
        //check if it is taken

        //Loop for every obtsacle and goal object
        //Loop for every object in array and then set segment to minus one

        if (run_positioning == true)
        {

            ResetOtherObjects();

            points_.CollectPoints();

            fuzzy_box.Setup_Fuzzy_Rules(0);

            run_positioning = false;
            check_time = true;

            float newValue = starting_sess_time - current_sess_time;
            Timings.Add(newValue);
            Debug.Log(newValue);
            StartTiming = true;
            //Segment = -1;
        }

        CollisionText.text = "Number of Resets : " +  num_of_cols.ToString();

    }


    public void ResetBox()
    {
        box_obj.transform.position = new Vector3(0f, 0.6f, 0.32f);
        Rigidbody rigidbody_;
        rigidbody_ = box_obj.GetComponent<Rigidbody>();
        rigidbody_.velocity = new Vector3(0f, 0f, 0f);
        rigidbody_.angularVelocity = new Vector3(0f, 0f, 0f);
        box_obj.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        //Segment = Random.Range(1, 4);
        run_positioning = true;

        check_time = false;
        currentTime = StartingTime;
        //points_.ResetPoints();
        //Segment_1_taken = false;
        //Segment_2_taken = false;
        //Segment_3_taken = false;
        //Segment_4_taken = false;

        points_.AddPoints();
        //Add points
    }

    public void ResetOtherObjects()
    {
        for (int j = 0; j < array_obj.Count; j++)
        {

            //Debug.Log(j);

            float x_value = 0.0f;
            float z_value = 0.0f;

            //switch (Segment)
            //{
            //    case 1:
            //        if (!Segment_1_taken)
            //        {
            //            x_value = Random.Range(-20, 20);
            //            z_value = Random.Range(10, 40);
            //            Segment_1_taken = true;
            //        }
            //        else
            //        {
            //            Segment = 2;
            //        }
            //        break;
            //    case 2:
            //        if (!Segment_2_taken)
            //        {
            //            x_value = Random.Range(10, 40);
            //            z_value = Random.Range(10, 40);
            //            Segment_2_taken = true;
            //        }
            //        else
            //        {
            //            Segment = 3;
            //        }
            //        break;
            //    case 3:
            //        if (!Segment_3_taken)
            //        {
            //            x_value = Random.Range(-20, -10);
            //            z_value = Random.Range(-20, -10);
            //            Segment_3_taken = true;
            //        }
            //        else
            //        {
            //            Segment = 4;
            //        }
            //        break;
            //    case 4:
            //        if (!Segment_4_taken)
            //        {
            //            x_value = Random.Range(20, 40);
            //            z_value = Random.Range(-20, -10);
            //            Segment_4_taken = true;
            //        }
            //        else
            //        {
            //            Segment = 1;
            //        }
            //        break;
            //}

            //Need to check if this is okay

            z_value = Random.Range(-20, 40);
            x_value = Random.Range(-20, 40);

            //if(Segment ==1)
            //{
            //    if (!Segment_1_taken)
            //    {
            //        x_value = Random.Range(-20, 20);
            //        z_value = Random.Range(10, 40);
            //        Segment_1_taken = true;
            //    }
            //    else
            //    {
            //        Segment = 2;
            //    }
            //}
            //if (Segment == 2)
            //{
            //    if (!Segment_2_taken)
            //    {
            //        x_value = Random.Range(10, 40);
            //        z_value = Random.Range(10, 40);
            //        Segment_2_taken = true;
            //    }
            //    else
            //    {
            //        Segment = 3;
            //    }
            //}
            //if(Segment==3)
            //{
            //    if (!Segment_3_taken)
            //    {
            //        x_value = Random.Range(-20, -10);
            //        z_value = Random.Range(-20, -10);
            //        Segment_3_taken = true;
            //    }
            //    else
            //    {
            //        Segment = 4;
            //    }
            //}
            //if(Segment == 4)
            //{
            //    if (!Segment_4_taken)
            //    {
            //        x_value = Random.Range(20, 40);
            //        z_value = Random.Range(-20, -10);
            //        Segment_4_taken = true;
            //    }
            //    //Dont Need to move along cause there will be at least two free sections so it should be fine
            //}

            array_obj[j].transform.position = new Vector3(x_value, 2.0f, z_value);

            had_collision = false;

            //Segment = Random.Range(1, 4);

        }
    }

    public void ResetCollisions()
    {
        num_of_cols = 0.0f;
        had_collision = false;
    }

    public void CheckTimings()
    {
        if(StartTiming == true)
        {
            starting_sess_time = time_.GiveCurrentTime();
            current_sess_time = starting_sess_time;
            StartTiming = false;
        }

        if (StartTiming == false && run_positioning == false)
        {
            current_sess_time -= 1 * Time.deltaTime;
        }
    }
}
