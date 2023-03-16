using FLS;
using FLS.Rules;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyBox : MonoBehaviour
{

	bool selected = false;

	//Need to fold the multiple engines into one

	IFuzzyEngine engineX;
	LinguisticVariable distance_X;
	LinguisticVariable direction_X;

	//-----------------------------

	//Try initialy with only distance for avoidance and we can then
	//see later if direcion is needed.

	//LinguisticVariable Avoidance_direction_X;

	IFuzzyEngine avoidEngineX;
	LinguisticVariable Avoidance_distance_X;
	//-----------------------------

	IFuzzyEngine engineZ;
	LinguisticVariable distance_Z;
	LinguisticVariable direction_Z;

	//-----------------------------
	//LinguisticVariable Avoidance_direction_Z;
	//LinguisticVariable Avoidance_distance_Z;
	//-----------------------------

	IFuzzyEngine avoidEngineZ;
	LinguisticVariable Avoidance_distance_Z;

	public GameObject Centre;
	private float Centre_x;
	private float Centre_z;

	//public GameObject Obstacle;
	private float Obstacle_X;
	private float Obstacle_Z;

	private float Obstacle_X1;
	private float Obstacle_Z1;

	private int[] array;

	[SerializeField]
	private List<GameObject> obstacle_array;

	private static int array_pos;

	void Start()
	{

		array = new int[3];

		array_pos = 0;

		//Needs to be set initially and then run as otherwise fixed update will work with a null value
		//Have one set of values to make things easier

        Centre_x = Centre.transform.position.x;
        Centre_z = Centre.transform.position.z;

		//Obstacle_X = Obstacle.transform.position.x;
		//Obstacle_Z = Obstacle.transform.position.z;

		Obstacle_X = obstacle_array[0].transform.position.x;
		Obstacle_Z = obstacle_array[0].transform.position.z;

		/*var right = distance.MembershipFunctions.AddTrapezoid("right", -50, -50, -5, -1);
		var none = distance.MembershipFunctions.AddTrapezoid("none", -5, -0.5, 0.5, 5);
		var left = distance.MembershipFunctions.AddTrapezoid("left", 1, 5, 50, 50);*/

		// Here we need to setup the Fuzzy Inference System
		distance_X = new LinguisticVariable("distanceX");
        var right_X = distance_X.MembershipFunctions.AddTrapezoid("right_X", Centre_x - 75, Centre_x - 75, Centre_x - 5, Centre_x - 1);
        var none_X = distance_X.MembershipFunctions.AddTrapezoid("none_X", Centre_x - 10, Centre_x - 0.5, Centre_x + 0.5, Centre_x + 10);
        var left_X = distance_X.MembershipFunctions.AddTrapezoid("left_X", Centre_x + 1, Centre_x + 10, Centre_x + 75, Centre_x + 75);

        direction_X = new LinguisticVariable("directionX");
        var right_direction_X = direction_X.MembershipFunctions.AddTrapezoid("right_direction_X", Centre_x + -75, Centre_x + -75, Centre_x + -5, Centre_x + -1);
        //Below is a problem line
        var none_direction_X = direction_X.MembershipFunctions.AddTrapezoid("none_direction_X", Centre_x + -10, Centre_x + -0.5, Centre_x + 0.5, Centre_x + 10);
        //
        var left_direction_X = direction_X.MembershipFunctions.AddTrapezoid("left_direction_X", Centre_x + 1, Centre_x + 10, Centre_x + 75, Centre_x + 75);

        //-----------------------------
        //Will probably need to map out a new graph and values to allow for the player to act normal
        //Will see how using the distance values work if we change the rules so that we move away from it rather than moving towards it

        //Avoidance_distance_X = new LinguisticVariable("Avoidance_distanceX");
        //var right_avoidance_distance_x = Avoidance_distance_X.MembershipFunctions.AddTrapezoid("right_avoidance_distanceX", Obstacle_X + -75, Obstacle_X + -75, Obstacle_X + -5, Obstacle_X + -1);
        //var left_avoidance_distance_x = Avoidance_distance_X.MembershipFunctions.AddTrapezoid("left_avoidance_distanceX", Obstacle_X + 1, Obstacle_X + 10, Obstacle_X + 75, Obstacle_X + 75);
        //-----------------------------

        engineX = new FuzzyEngineFactory().Default();

        var rule1_X = Rule.If(distance_X.Is(right_X)).Then(direction_X.Is(left_direction_X));
        var rule2_X = Rule.If(distance_X.Is(left_X)).Then(direction_X.Is(right_direction_X));
        var rule3_X = Rule.If(distance_X.Is(none_X)).Then(direction_X.Is(none_direction_X));

        //-----------------------------
        //var rule4_X = Rule.If(Avoidance_distance_X.Is(right_avoidance_distance_x)).Then(direction_X.Is(right_direction_X));
        //var rule5_X = Rule.If(Avoidance_distance_X.Is(left_avoidance_distance_x)).Then(direction_X.Is(left_direction_X));
        //-----------------------------

        engineX.Rules.Add(rule1_X, rule2_X, rule3_X);
        //engineX.Rules.Add(rule1_X, rule2_X, rule3_X,rule4_X,rule5_X);

        //Issues adding rules to the engine for some reason.

        //engineX.Rules.Add(rule4_X, rule5_X);


        //-----------------------------

        Avoidance_distance_X = new LinguisticVariable("Avoidance_distanceX");
        var right_avoidance_distance_x = Avoidance_distance_X.MembershipFunctions.AddTrapezoid("right_avoidance_distanceX", Obstacle_X + -12.5, Obstacle_X + -12.5, Obstacle_X + -5, Obstacle_X + -1);
        var left_avoidance_distance_x = Avoidance_distance_X.MembershipFunctions.AddTrapezoid("left_avoidance_distanceX", Obstacle_X + 0.25, Obstacle_X + 1.25, Obstacle_X + 12.5, Obstacle_X + 12.5);


        avoidEngineX = new FuzzyEngineFactory().Default();

        var rule4_X = Rule.If(Avoidance_distance_X.Is(right_avoidance_distance_x)).Then(direction_X.Is(right_direction_X));
        var rule5_X = Rule.If(Avoidance_distance_X.Is(left_avoidance_distance_x)).Then(direction_X.Is(left_direction_X));

        avoidEngineX.Rules.Add(rule4_X, rule5_X);

        //-----------------------------

        // Here we need to setup the Fuzzy Inference System
        distance_Z = new LinguisticVariable("distanceZ");
        var right_Z = distance_Z.MembershipFunctions.AddTrapezoid("right_Y", Centre_z - 75, Centre_z - 75, Centre_z - 5, Centre_z - 1);
        var none_Z = distance_Z.MembershipFunctions.AddTrapezoid("none_Y", Centre_z - 10, Centre_z - 0.5, Centre_z + 0.5, Centre_z + 10);
        var left_Z = distance_Z.MembershipFunctions.AddTrapezoid("left_Y", Centre_z + 1, Centre_z + 10, Centre_z + 75, Centre_z + 75);

        direction_Z = new LinguisticVariable("directionZ");
        var right_direction_Z = direction_Z.MembershipFunctions.AddTrapezoid("right_direction_Y", Centre_z + -75, Centre_z + -75, Centre_z + -5, Centre_z + -1);
        var none_direction_Z = direction_Z.MembershipFunctions.AddTrapezoid("none_direction_Y", Centre_z + -10, Centre_z + -0.5, Centre_z + 0.5, Centre_z + 10);
        var left_direction_Z = direction_Z.MembershipFunctions.AddTrapezoid("left_direction_Y", Centre_z + 1, Centre_z + 10, Centre_z + 75, Centre_z + 75);

        engineZ = new FuzzyEngineFactory().Default();

        var rule1_Z = Rule.If(distance_Z.Is(right_Z)).Then(direction_Z.Is(left_direction_Z));
        var rule2_Z = Rule.If(distance_Z.Is(left_Z)).Then(direction_Z.Is(right_direction_Z));
        var rule3_Z = Rule.If(distance_Z.Is(none_Z)).Then(direction_Z.Is(none_direction_Z));

        engineZ.Rules.Add(rule1_Z, rule2_Z, rule3_Z);

        //-----------------------------

        Avoidance_distance_Z = new LinguisticVariable("Avoidance_distanceZ");
        var right_avoidance_distance_z = Avoidance_distance_Z.MembershipFunctions.AddTrapezoid("right_avoidance_distanceZ", Obstacle_Z + -12.5, Obstacle_Z + -12.5, Obstacle_Z + -5, Obstacle_Z + -1);
        var left_avoidance_distance_z = Avoidance_distance_Z.MembershipFunctions.AddTrapezoid("left_avoidance_distanceZ", Obstacle_Z + 0.25, Obstacle_Z + 1.25, Obstacle_Z + 12.5, Obstacle_Z + 12.5);

        avoidEngineZ = new FuzzyEngineFactory().Default();

        var rule4_Z = Rule.If(Avoidance_distance_Z.Is(right_avoidance_distance_z)).Then(direction_Z.Is(right_direction_Z));
        var rule5_Z = Rule.If(Avoidance_distance_Z.Is(left_avoidance_distance_z)).Then(direction_Z.Is(left_direction_Z));

        avoidEngineZ.Rules.Add(rule4_Z, rule5_Z);

        //-----------------------------

    }

    void FixedUpdate()
	{
        //if (!selected && this.transform.position.y < 0.6f)
        //{
            // Convert position of box to value between 0 and 100
			double resultX = 0.0;
			double resultZ = 0.0;

			double new_result_X = 0.0;
			double new_result_Z = 0.0;

			double speed_ = 0.75;

			resultX = engineX.Defuzzify(new { distanceX = (double)this.transform.position.x });
			resultZ = engineZ.Defuzzify(new { distanceZ = (double)this.transform.position.z });

			new_result_X = avoidEngineX.Defuzzify(new { Avoidance_distanceX = (double)this.transform.position.x });
			new_result_Z = avoidEngineZ.Defuzzify(new { Avoidance_distanceZ = (double)this.transform.position.z });

			double complete_resultX = resultX + new_result_X;

			double complete_resultZ = resultZ + new_result_Z;

			//Debug.Log("Result X : " + resultX);
			//Debug.Log("Result Z : " + resultZ);
			//Debug.Log(speed_result_z);

			Rigidbody rigidbody = GetComponent<Rigidbody>();
			rigidbody.AddForce(new Vector3((float)(complete_resultX * speed_), 0f, (float)(complete_resultZ * speed_)));
        //}
    }

    // Update is called once per frame
    void Update()
	{
        //if (Input.GetMouseButtonDown(0))
        //{
        //    var hit = new RaycastHit();
        //    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        if (hit.transform.name == "FuzzyBox") Debug.Log("You have clicked the FuzzyBox");
        //        selected = true;
        //    }
        //}

        //if (Input.GetMouseButton(0) && selected)
        //{
        //    float distanceToScreen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        //    Vector3 curPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen));
        //    transform.position = new Vector3(curPosition.x, Mathf.Max(0.5f, curPosition.y), transform.position.z);
        //}

        //if (Input.GetMouseButtonUp(0))
        //{
        //    selected = false;
        //}

		//Need to cycle through obstacles as we update the rules and variables.
		//static integer which is consitent with the place in the array.
		//go from zero and at the start of the update if the value of the array we are checking is ever greater than the size of the array we reset it back to zero.


		if(array_pos > obstacle_array.Count - 1)
        {
			array_pos = 0;
        }

		Debug.Log(array_pos);

		Centre_x = Centre.transform.position.x;
		Centre_z = Centre.transform.position.z;

		Obstacle_X1 = obstacle_array[array_pos].transform.position.x;
		Obstacle_Z1 = obstacle_array[array_pos].transform.position.z;

		//Obstacle_X = Obstacle.transform.position.x;
		//Obstacle_Z = Obstacle.transform.position.z;

		/*var right = distance.MembershipFunctions.AddTrapezoid("right", -50, -50, -5, -1);
		var none = distance.MembershipFunctions.AddTrapezoid("none", -5, -0.5, 0.5, 5);
		var left = distance.MembershipFunctions.AddTrapezoid("left", 1, 5, 50, 50);*/

		// Here we need to setup the Fuzzy Inference System
		distance_X = new LinguisticVariable("distanceX");
		var right_X = distance_X.MembershipFunctions.AddTrapezoid("right_X", Centre_x - 75, Centre_x - 75, Centre_x - 5, Centre_x - 1);
		var none_X = distance_X.MembershipFunctions.AddTrapezoid("none_X", Centre_x - 10, Centre_x - 0.5, Centre_x + 0.5, Centre_x + 10);
		var left_X = distance_X.MembershipFunctions.AddTrapezoid("left_X", Centre_x + 1, Centre_x + 10, Centre_x + 75, Centre_x + 75);

		direction_X = new LinguisticVariable("directionX");
		var right_direction_X = direction_X.MembershipFunctions.AddTrapezoid("right_direction_X", Centre_x + -75, Centre_x + -75, Centre_x + -5, Centre_x + -1);
		//Below is a problem line
		var none_direction_X = direction_X.MembershipFunctions.AddTrapezoid("none_direction_X", Centre_x + -10, Centre_x + -0.5, Centre_x + 0.5, Centre_x + 10);
		//
		var left_direction_X = direction_X.MembershipFunctions.AddTrapezoid("left_direction_X", Centre_x + 1, Centre_x + 10, Centre_x + 75, Centre_x + 75);

		//-----------------------------
		//Will probably need to map out a new graph and values to allow for the player to act normal
		//Will see how using the distance values work if we change the rules so that we move away from it rather than moving towards it

		//Avoidance_distance_X = new LinguisticVariable("Avoidance_distanceX");
		//var right_avoidance_distance_x = Avoidance_distance_X.MembershipFunctions.AddTrapezoid("right_avoidance_distanceX", Obstacle_X + -75, Obstacle_X + -75, Obstacle_X + -5, Obstacle_X + -1);
		//var left_avoidance_distance_x = Avoidance_distance_X.MembershipFunctions.AddTrapezoid("left_avoidance_distanceX", Obstacle_X + 1, Obstacle_X + 10, Obstacle_X + 75, Obstacle_X + 75);
		//-----------------------------

		engineX = new FuzzyEngineFactory().Default();

		var rule1_X = Rule.If(distance_X.Is(right_X)).Then(direction_X.Is(left_direction_X));
		var rule2_X = Rule.If(distance_X.Is(left_X)).Then(direction_X.Is(right_direction_X));
		var rule3_X = Rule.If(distance_X.Is(none_X)).Then(direction_X.Is(none_direction_X));

		//-----------------------------
		//var rule4_X = Rule.If(Avoidance_distance_X.Is(right_avoidance_distance_x)).Then(direction_X.Is(right_direction_X));
		//var rule5_X = Rule.If(Avoidance_distance_X.Is(left_avoidance_distance_x)).Then(direction_X.Is(left_direction_X));
		//-----------------------------

		engineX.Rules.Add(rule1_X, rule2_X, rule3_X);
        //engineX.Rules.Add(rule1_X, rule2_X, rule3_X,rule4_X,rule5_X);

        //Issues adding rules to the engine for some reason.

        //engineX.Rules.Add(rule4_X, rule5_X);


        //-----------------------------

        Avoidance_distance_X = new LinguisticVariable("Avoidance_distanceX");
        var right_avoidance_distance_x = Avoidance_distance_X.MembershipFunctions.AddTrapezoid("right_avoidance_distanceX", Obstacle_X1 + -12.5, Obstacle_X1 + -12.5, Obstacle_X1 + -5, Obstacle_X1 + -1);
        var left_avoidance_distance_x = Avoidance_distance_X.MembershipFunctions.AddTrapezoid("left_avoidance_distanceX", Obstacle_X1 + 0.25, Obstacle_X1 + 1.25, Obstacle_X1 + 12.5, Obstacle_X1 + 12.5);


		avoidEngineX = new FuzzyEngineFactory().Default();

        var rule4_X = Rule.If(Avoidance_distance_X.Is(right_avoidance_distance_x)).Then(direction_X.Is(right_direction_X));
        var rule5_X = Rule.If(Avoidance_distance_X.Is(left_avoidance_distance_x)).Then(direction_X.Is(left_direction_X));

		avoidEngineX.Rules.Add(rule4_X, rule5_X);

        //-----------------------------

        // Here we need to setup the Fuzzy Inference System
        distance_Z = new LinguisticVariable("distanceZ");
		var right_Z = distance_Z.MembershipFunctions.AddTrapezoid("right_Y", Centre_z - 75, Centre_z - 75, Centre_z - 5, Centre_z - 1);
		var none_Z = distance_Z.MembershipFunctions.AddTrapezoid("none_Y", Centre_z - 10, Centre_z - 0.5, Centre_z + 0.5, Centre_z + 10);
		var left_Z = distance_Z.MembershipFunctions.AddTrapezoid("left_Y", Centre_z + 1, Centre_z + 10, Centre_z + 75, Centre_z + 75);

		direction_Z = new LinguisticVariable("directionZ");
		var right_direction_Z = direction_Z.MembershipFunctions.AddTrapezoid("right_direction_Y", Centre_z + -75, Centre_z + -75, Centre_z + -5, Centre_z + -1);
		var none_direction_Z = direction_Z.MembershipFunctions.AddTrapezoid("none_direction_Y", Centre_z + -10, Centre_z + -0.5, Centre_z + 0.5, Centre_z + 10);
		var left_direction_Z = direction_Z.MembershipFunctions.AddTrapezoid("left_direction_Y", Centre_z + 1, Centre_z + 10, Centre_z + 75, Centre_z + 75);

		engineZ = new FuzzyEngineFactory().Default();

		var rule1_Z = Rule.If(distance_Z.Is(right_Z)).Then(direction_Z.Is(left_direction_Z));
		var rule2_Z = Rule.If(distance_Z.Is(left_Z)).Then(direction_Z.Is(right_direction_Z));
		var rule3_Z = Rule.If(distance_Z.Is(none_Z)).Then(direction_Z.Is(none_direction_Z));

		engineZ.Rules.Add(rule1_Z, rule2_Z, rule3_Z);

		//-----------------------------

		Avoidance_distance_Z = new LinguisticVariable("Avoidance_distanceZ");
		var right_avoidance_distance_z = Avoidance_distance_Z.MembershipFunctions.AddTrapezoid("right_avoidance_distanceZ", Obstacle_Z1 + -12.5, Obstacle_Z1 + -12.5, Obstacle_Z1 + -5, Obstacle_Z1 + -1);
		var left_avoidance_distance_z = Avoidance_distance_Z.MembershipFunctions.AddTrapezoid("left_avoidance_distanceZ", Obstacle_Z1 + 0.25, Obstacle_Z1 + 1.25, Obstacle_Z1 + 12.5, Obstacle_Z1 + 12.5);

		avoidEngineZ = new FuzzyEngineFactory().Default();

		var rule4_Z = Rule.If(Avoidance_distance_Z.Is(right_avoidance_distance_z)).Then(direction_Z.Is(right_direction_Z));
		var rule5_Z = Rule.If(Avoidance_distance_Z.Is(left_avoidance_distance_z)).Then(direction_Z.Is(left_direction_Z));

		avoidEngineZ.Rules.Add(rule4_Z, rule5_Z);

		//-----------------------------



		array_pos++;
    }


}
