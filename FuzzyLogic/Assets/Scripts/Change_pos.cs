using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Change_pos : MonoBehaviour
{
    //Box Object
    [SerializeField]
    GameObject box_obj;

    //List of obstacle objects
    [SerializeField]
    private List<GameObject> obstacle_array;

    //Goal Object
    [SerializeField]
    GameObject GoalObject;

    //Game Manager Object
    [SerializeField]
    GameObject GameManager;
    //Point system script
    PointSystem points_;

    //Timer Object
    [SerializeField]
    GameObject timer;
    //Countdown script
    CountdownTimer time_;

    //List of all the obstacle objects and goal objects
    private List<GameObject> array_obj;

    //Variables of the current time and the starting time
    private float currentTime = 0;
    //This value also reprensts how long the box has to find the goal as if it doesnt
    //The whole thing resets and any points gained in that time are null and void
    private float StartingTime = 15;
    //Boolean to tel the script to check if the box object has reached the goal object
    private bool check_time;

    //Variable to count the number of collisions with the obstacles
    private float num_of_cols = 0.0f;
    //Boolean to say if the box has collided with the obstacle
    private bool had_collision = false;

    //Text to output the number of collisions
    [SerializeField]
    Text CollisionText;

    //Boolean to run the re-positioning code
    bool run_positioning;

    //USed to get the current instant of the fuzzy box object
    FuzzyBox fuzzy_box;

    //Used to keep a list of timings
    public List<float> Timings;
    //Current time of the session (which is what I used to call each time the objects get repositioned)
    private float current_sess_time;
    //Starting time of the session
    private float starting_sess_time;
    //Boolean to start the timing logic for this session
    private bool StartTiming;

    //Start function
    void Start()
    {
        //Initialises the variables
        currentTime = StartingTime;
        check_time = false;

        array_obj = new List<GameObject>();

        //Sets each script object to be the correct instance from an object
        fuzzy_box = box_obj.GetComponent<FuzzyBox>();
        points_ = GameManager.GetComponent<PointSystem>();
        time_ = timer.GetComponent<CountdownTimer>();

        current_sess_time = 0;
        starting_sess_time = 0;
        StartTiming = true;

        run_positioning = false;

        //Puts all the current objects into an array of objects which can be used to loop through tem all later
        array_obj.Add(GoalObject);
        array_obj.Add(obstacle_array[0]);
    }

    // Update is called once per frame
    void Update()
    {
        CheckTimings();

        //Check if the box object and the obstacle are within a certain distance of each other
        if (Vector3.Distance(box_obj.transform.position, obstacle_array[0].transform.position) < 3)
        {
            //Checks if the box has already had a collision with this object
            if(had_collision == false)
            {
                //Deduct points
                points_.MinusPoints();
                //Increases the number of collisions
                num_of_cols++;
                //Sets the boolean to true so that this will not be runagain this session to create a great increase in collisions
                had_collision = true;
            }
        }

        //Check if the box object and the goal are within a certain distance of each other
        if (Vector3.Distance(box_obj.transform.position, GoalObject.transform.position) < 6.125f)
        {
            ResetBox();
        }

        //Checks the check time varialce
        if (check_time == true)
        {
            //Counts down the clock
            currentTime -= 1 * Time.deltaTime;
            //If the timer reaches zero or below the whole thing will reset
            if(currentTime <= 0.0f)
            {
                ResetBox();
            }
        }

        //Checks if the script should run the reposition functions
        if (run_positioning == true)
        {
            ResetOtherObjects();

            points_.CollectPoints();

            fuzzy_box.Setup_Fuzzy_Rules(0);

            //Resets the booleans
            run_positioning = false;
            check_time = true;
            had_collision = false;

            //Adds the time it took to get to the goal to a list
            float newValue = starting_sess_time - current_sess_time;
            Timings.Add(newValue);
            StartTiming = true;
        }

        //Outputs the number of resets to a text element
        CollisionText.text = "Number of Resets : " +  num_of_cols.ToString();
    }


    public void ResetBox()
    {
        //Resets the box object aswell as any forces acting on the body
        box_obj.transform.position = new Vector3(0f, 0.6f, 0.32f);
        Rigidbody rigidbody_;
        rigidbody_ = box_obj.GetComponent<Rigidbody>();
        rigidbody_.velocity = new Vector3(0f, 0f, 0f);
        rigidbody_.angularVelocity = new Vector3(0f, 0f, 0f);
        box_obj.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

        //resets the current time value and the other booleans
        run_positioning = true;

        check_time = false;
        currentTime = StartingTime;

        //Add points
        points_.AddPoints();
    }

    public void ResetOtherObjects()
    {
        //Loops through every object in the array
        //and chooses a random space on the plane within the range of -20 to 40
        //in both the x and z axis and sets thats to be it snew position
        for (int j = 0; j < array_obj.Count; j++)
        {
            float x_value = 0.0f;
            float z_value = 0.0f;

            z_value = Random.Range(-20, 40);
            x_value = Random.Range(-20, 40);

            array_obj[j].transform.position = new Vector3(x_value, 2.0f, z_value);

        }
    }

    //Reset the collision varaibles
    public void ResetCollisions()
    {
        num_of_cols = 0.0f;
        had_collision = false;
    }

    public void CheckTimings()
    {
        //Checks if the the session timing should start
        if(StartTiming == true)
        {
            //Sets the starting session time to be that of the current time
            starting_sess_time = time_.GiveCurrentTime();
            //And the current session time to the starting session time
            current_sess_time = starting_sess_time;
            //Resets the boolean
            StartTiming = false;
        }

        //If the the above has been run and initialised and we are not repositioning objects
        if (StartTiming == false && run_positioning == false)
        {
            //Countdown the timer for the current session time
            current_sess_time -= 1 * Time.deltaTime;
        }
    }
}
