//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Change_pos : MonoBehaviour
//{
//    [SerializeField]
//    GameObject box_obj;
//    [SerializeField]
//    private List<GameObject> obstacle_array;
//    [SerializeField]
//    GameObject GoalObject;

//    Rigidbody GoalBody;
//    Rigidbody ObstacleBody_1;
//    Rigidbody ObstacleBody_2;

//    // Start is called before the first frame update
//    void Start()
//    {
//        GoalBody = GoalObject.GetComponent<Rigidbody>();
//        ObstacleBody_1 = obstacle_array[0].GetComponent<Rigidbody>();
//        ObstacleBody_2 = obstacle_array[1].GetComponent<Rigidbody>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        GoalMove();
//        Obstavle1Move();
//        Obstavle2Move();
//    }


//    private void GoalMove()
//    {
//        if (Input.GetKey(KeyCode.UpArrow))
//        {
//            GoalBody.AddForce(new Vector3(0, 0, 10.0f));
//        }

//        if (Input.GetKey(KeyCode.DownArrow))
//        {
//            GoalBody.AddForce(new Vector3(0, 0, -10.0f));
//        }

//        if (Input.GetKey(KeyCode.LeftArrow))
//        {
//            GoalBody.AddForce(new Vector3(-10.0f, 0, 0));
//        }

//        if (Input.GetKey(KeyCode.RightArrow))
//        {
//            GoalBody.AddForce(new Vector3(10.0f, 0, 0));
//        }
//    }

//    private void Obstavle1Move()
//    {
//        if (Input.GetKey(KeyCode.T))
//        {
//            ObstacleBody_1.AddForce(new Vector3(0, 0, 10.0f));
//        }

//        if (Input.GetKey(KeyCode.G))
//        {
//            ObstacleBody_1.AddForce(new Vector3(0, 0, -10.0f));
//        }

//        if (Input.GetKey(KeyCode.F))
//        {
//            ObstacleBody_1.AddForce(new Vector3(-10.0f, 0, 0));
//        }

//        if (Input.GetKey(KeyCode.H))
//        {
//            ObstacleBody_1.AddForce(new Vector3(10.0f, 0, 0));
//        }
//    }

//    private void Obstavle2Move()
//    {
//        if (Input.GetKey(KeyCode.I))
//        {
//            ObstacleBody_2.AddForce(new Vector3(0, 0, 10.0f));
//        }

//        if (Input.GetKey(KeyCode.K))
//        {
//            ObstacleBody_2.AddForce(new Vector3(0, 0, -10.0f));
//        }

//        if (Input.GetKey(KeyCode.J))
//        {
//            ObstacleBody_2.AddForce(new Vector3(-10.0f, 0, 0));
//        }

//        if (Input.GetKey(KeyCode.L))
//        {
//            ObstacleBody_2.AddForce(new Vector3(10.0f, 0, 0));
//        }
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private List<GameObject> array_obj;

    //private List<int> array_obj_space;

    //int Segment;

    bool run_positioning;

    //bool Segment_1_taken;
    //bool Segment_2_taken;
    //bool Segment_3_taken;
    //bool Segment_4_taken;

    FuzzyBox fuzzy_box;

    //Need to find a way to change positions of all the objects once the box has got close enough to the goal
    // The obstacles should change positions
    // The goal should change position based randomly from the centre
    // Can divide up from the centre into quadrants
    // The player should reset to the centre - if its between a certain area or if it stops for a while - DONE

    // Start is called before the first frame update
    void Start()
    {
        array_obj = new List<GameObject>();

        fuzzy_box = box_obj.GetComponent<FuzzyBox>();

        points_ = GameManager.GetComponent<PointSystem>();

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
        if(Vector3.Distance(box_obj.transform.position, obstacle_array[0].transform.position) < 1)
        {
            //Deduct points
            points_.MinusPoints();
        }

        if (Vector3.Distance(box_obj.transform.position, GoalObject.transform.position) < 6.125f)
        {
            ResetBox();
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
            //Segment = -1;
        }


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

            Debug.Log(j);

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

            //Segment = Random.Range(1, 4);

        }
    }
}
