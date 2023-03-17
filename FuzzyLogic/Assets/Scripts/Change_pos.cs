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

    Rigidbody GoalBody;
    Rigidbody ObstacleBody_1;
    Rigidbody ObstacleBody_2;

    // Start is called before the first frame update
    void Start()
    {
        GoalBody = GoalObject.GetComponent<Rigidbody>();
        ObstacleBody_1 = obstacle_array[0].GetComponent<Rigidbody>();
        ObstacleBody_2 = obstacle_array[1].GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GoalMove();
        Obstavle1Move();
        Obstavle2Move();
    }


    private void GoalMove()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            GoalBody.AddForce(new Vector3(0, 0, 10.0f));
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            GoalBody.AddForce(new Vector3(0, 0, -10.0f));
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            GoalBody.AddForce(new Vector3(-10.0f, 0, 0));
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            GoalBody.AddForce(new Vector3(10.0f, 0, 0));
        }
    }

    private void Obstavle1Move()
    {
        if (Input.GetKey(KeyCode.T))
        {
            ObstacleBody_1.AddForce(new Vector3(0, 0, 10.0f));
        }

        if (Input.GetKey(KeyCode.G))
        {
            ObstacleBody_1.AddForce(new Vector3(0, 0, -10.0f));
        }

        if (Input.GetKey(KeyCode.F))
        {
            ObstacleBody_1.AddForce(new Vector3(-10.0f, 0, 0));
        }

        if (Input.GetKey(KeyCode.H))
        {
            ObstacleBody_1.AddForce(new Vector3(10.0f, 0, 0));
        }
    }

    private void Obstavle2Move()
    {
        if (Input.GetKey(KeyCode.I))
        {
            ObstacleBody_2.AddForce(new Vector3(0, 0, 10.0f));
        }

        if (Input.GetKey(KeyCode.K))
        {
            ObstacleBody_2.AddForce(new Vector3(0, 0, -10.0f));
        }

        if (Input.GetKey(KeyCode.J))
        {
            ObstacleBody_2.AddForce(new Vector3(-10.0f, 0, 0));
        }

        if (Input.GetKey(KeyCode.L))
        {
            ObstacleBody_2.AddForce(new Vector3(10.0f, 0, 0));
        }
    }
}


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

//    private List<GameObject> array_obj;

//    int Segment;

//    bool run_positioning;

//    bool Segment_1_taken;
//    bool Segment_2_taken;
//    bool Segment_3_taken;
//    bool Segment_4_taken;

//    //Need to find a way to change positions of all the objects once the box has got close enough to the goal
//    // The obstacles should change positions
//    // The goal should change position based randomly from the centre
//    // Can divide up from the centre into quadrants
//    // The player should reset to the centre - if its between a certain area or if it stops for a while - DONE

//    // Start is called before the first frame update
//    void Start()
//    {
//        array_obj = new List<GameObject>();


//        Segment = -1;

//        run_positioning = false;
//        Segment_1_taken = false;
//        Segment_2_taken = false;
//        Segment_3_taken = false;
//        Segment_4_taken = false;

//        array_obj.Add(GoalObject);
//        array_obj.Add(obstacle_array[0]);
//        //array_obj.Add(obstacle_array[1]);

//        //for(int i=0; i< obstacle_array.Count;i++)
//        //{
//        //    array_obj.Add(obstacle_array[i]);
//        //}
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Vector3.Distance(box_obj.transform.position, GoalObject.transform.position) < 7.25f)
//        {
//            box_obj.transform.position = new Vector3(0f, 0.6f, 0.32f);
//            Rigidbody rigidbody_;
//            rigidbody_ = box_obj.GetComponent<Rigidbody>();
//            rigidbody_.velocity = new Vector3(0f, 0f, 0f);
//            rigidbody_.angularVelocity = new Vector3(0f, 0f, 0f);
//            box_obj.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));

//            Segment = Random.Range(1, 4);
//            run_positioning = true;
//            Segment_1_taken = false;
//            Segment_2_taken = false;
//            Segment_3_taken = false;
//            Segment_4_taken = false;
//        }

//        //Loop for every obstacle and goal
//        //check if it is taken

//        //Loop for every obtsacle and goal object
//        //Loop for every object in array and then set segment to minus one

//        if (run_positioning == true)
//        {

//            for (int j = 0; j < array_obj.Count; j++)
//            {

//                float x_value = 0.0f;
//                float z_value = 0.0f;

//                switch (Segment)
//                {
//                    case 1:
//                        if (!Segment_1_taken)
//                        {
//                            x_value = Random.Range(-20, 20);
//                            z_value = Random.Range(10, 40);
//                            Segment_1_taken = true;
//                        }
//                        break;
//                    case 2:
//                        if (!Segment_2_taken)
//                        {
//                            x_value = Random.Range(10, 40);
//                            z_value = Random.Range(10, 40);
//                            Segment_2_taken = true;
//                        }
//                        break;
//                    case 3:
//                        if (!Segment_3_taken)
//                        {
//                            x_value = Random.Range(-20, -10);
//                            z_value = Random.Range(-20, -10);
//                            Segment_3_taken = true;
//                        }
//                        break;
//                    case 4:
//                        if (!Segment_4_taken)
//                        {
//                            x_value = Random.Range(20, 40);
//                            z_value = Random.Range(-20, -10);
//                            Segment_4_taken = true;
//                        }
//                        break;
//                }

//                array_obj[j].transform.position = new Vector3(x_value, 2.0f, z_value);

//                Segment = Random.Range(1, 4);

//            }

//            run_positioning = false;
//            Segment = -1;
//        }


//    }
//}
