using FLS;
using FLS.Rules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyBox : MonoBehaviour
{
	IFuzzyEngine engineX;
	LinguisticVariable distance_X;
	LinguisticVariable direction_X;

	IFuzzyEngine avoidEngineX;
	LinguisticVariable Avoidance_distance_X;

	IFuzzyEngine engineZ;
	LinguisticVariable distance_Z;
	LinguisticVariable direction_Z;

	IFuzzyEngine avoidEngineZ;
	LinguisticVariable Avoidance_distance_Z;

	public GameObject Centre;
	private float Centre_x;
	private float Centre_z;

	private float Obstacle_X;
	private float Obstacle_Z;

	private float Obstacle_X1;
	private float Obstacle_Z1;

	private static int array_pos;

	double resultX;
	double resultZ;

	double new_result_X;
	double new_result_Z;

	private float diffrence_x;
	private float diffrence_z;

	double speed_ = 0.75;

	double complete_resultX;
	double complete_resultZ;

	[SerializeField]
	private List<GameObject> obstacle_array;

	void Start()
	{
		array_pos = 0;
		Setup_Fuzzy_Rules(array_pos);
	}

    void FixedUpdate()
	{
		resultX = 0.0;
		resultZ = 0.0;

		new_result_X = 0.0;
		new_result_Z = 0.0;

		speed_ = 0.45;

		resultX = engineX.Defuzzify(new { distanceX = (double)this.transform.position.x });
		resultZ = engineZ.Defuzzify(new { distanceZ = (double)this.transform.position.z });

		new_result_X = avoidEngineX.Defuzzify(new { Avoidance_distanceX = (double)this.transform.position.x });
		new_result_Z = avoidEngineZ.Defuzzify(new { Avoidance_distanceZ = (double)this.transform.position.z });

		complete_resultX = resultX + new_result_X;

		complete_resultZ = resultZ + new_result_Z;

		//Debug.Log("Result X : " + resultX);
		//Debug.Log("Result Z : " + resultZ);
		//Debug.Log(speed_result_z);

		Rigidbody rigidbody = GetComponent<Rigidbody>();
		rigidbody.AddForce(new Vector3((float)(complete_resultX * speed_), 0f, (float)(complete_resultZ * speed_)));
    }

    // Update is called once per frame
    void Update()
	{
		if(array_pos > obstacle_array.Count - 1)
        {
			array_pos = 0;
        }

		Setup_Fuzzy_Rules(array_pos);

		Debug.Log(array_pos);

		array_pos++;
    }

	void Setup_Fuzzy_Rules(int pos_in_arr)
    {
		Centre_x = Centre.transform.position.x;
		Centre_z = Centre.transform.position.z;

		diffrence_x = 100;

		diffrence_z = 100;

		Obstacle_X1 = obstacle_array[pos_in_arr].transform.position.x;
		Obstacle_Z1 = obstacle_array[pos_in_arr].transform.position.z;

		/*var right = distance.MembershipFunctions.AddTrapezoid("right", -50, -50, -5, -1);
		var none = distance.MembershipFunctions.AddTrapezoid("none", -5, -0.5, 0.5, 5);
		var left = distance.MembershipFunctions.AddTrapezoid("left", 1, 5, 50, 50);*/

		// Here we need to setup the Fuzzy Inference System
		distance_X = new LinguisticVariable("distanceX");
		var right_X = distance_X.MembershipFunctions.AddTrapezoid("right_X", Centre_x - diffrence_x, Centre_x - diffrence_x, Centre_x - 5, Centre_x - 1);
		var none_X = distance_X.MembershipFunctions.AddTrapezoid("none_X", Centre_x - 10, Centre_x - 0.5, Centre_x + 0.5, Centre_x + 10);
		var left_X = distance_X.MembershipFunctions.AddTrapezoid("left_X", Centre_x + 1, Centre_x + 10, Centre_x + diffrence_x, Centre_x + diffrence_x);

		direction_X = new LinguisticVariable("directionX");
		var right_direction_X = direction_X.MembershipFunctions.AddTrapezoid("right_direction_X", Centre_x + -diffrence_x, Centre_x + -diffrence_x, Centre_x + -5, Centre_x + -1);
		var none_direction_X = direction_X.MembershipFunctions.AddTrapezoid("none_direction_X", Centre_x + -10, Centre_x + -0.5, Centre_x + 0.5, Centre_x + 10);
		var left_direction_X = direction_X.MembershipFunctions.AddTrapezoid("left_direction_X", Centre_x + 1, Centre_x + 10, Centre_x + diffrence_x, Centre_x + diffrence_x);

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

		engineX.Rules.Add(rule1_X, rule2_X, rule3_X);

		//-----------------------------
		Avoidance_distance_X = new LinguisticVariable("Avoidance_distanceX");
		var right_avoidance_distance_x = Avoidance_distance_X.MembershipFunctions.AddTrapezoid("right_avoidance_distanceX", Obstacle_X1 + -12.5, Obstacle_X1 + -12.5, Obstacle_X1 + -5, Obstacle_X1 + -1);
		var left_avoidance_distance_x = Avoidance_distance_X.MembershipFunctions.AddTrapezoid("left_avoidance_distanceX", Obstacle_X1 + 0.25, Obstacle_X1 + 1.25, Obstacle_X1 + 12.5, Obstacle_X1 + 12.5);

		avoidEngineX = new FuzzyEngineFactory().Default();

		var rule4_X = Rule.If(Avoidance_distance_X.Is(right_avoidance_distance_x)).Then(direction_X.Is(right_direction_X));
		var rule5_X = Rule.If(Avoidance_distance_X.Is(left_avoidance_distance_x)).Then(direction_X.Is(left_direction_X));

		avoidEngineX.Rules.Add(rule4_X, rule5_X);
		//-----------------------------

		distance_Z = new LinguisticVariable("distanceZ");
		var right_Z = distance_Z.MembershipFunctions.AddTrapezoid("right_Y", Centre_z - diffrence_z, Centre_z - diffrence_z, Centre_z - 5, Centre_z - 1);
		var none_Z = distance_Z.MembershipFunctions.AddTrapezoid("none_Y", Centre_z - 10, Centre_z - 0.5, Centre_z + 0.5, Centre_z + 10);
		var left_Z = distance_Z.MembershipFunctions.AddTrapezoid("left_Y", Centre_z + 1, Centre_z + 10, Centre_z + diffrence_z, Centre_z + diffrence_z);

		direction_Z = new LinguisticVariable("directionZ");
		var right_direction_Z = direction_Z.MembershipFunctions.AddTrapezoid("right_direction_Y", Centre_z + -diffrence_z, Centre_z + -diffrence_z, Centre_z + -5, Centre_z + -1);
		var none_direction_Z = direction_Z.MembershipFunctions.AddTrapezoid("none_direction_Y", Centre_z + -10, Centre_z + -0.5, Centre_z + 0.5, Centre_z + 10);
		var left_direction_Z = direction_Z.MembershipFunctions.AddTrapezoid("left_direction_Y", Centre_z + 1, Centre_z + 10, Centre_z + diffrence_z, Centre_z + diffrence_z);

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
	}


}
