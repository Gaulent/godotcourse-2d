using Godot;
using System;

public partial class GameManager : Node2D
{
	private static int Score = 0;

	public static void AddPoints()
	{
		Score++;
		GD.Print(Score);
	}
}



/*

	public int Score = 0;
	public Text text;
	
	// Start is called before the first frame update
	void Start()
	{
		text = FindObjectOfType<Text>();
	}

	// Update is called once per frame
	void Update()
	{
		text.text = Score.ToString();
	}

	public void AddPoints()
	{
		Score++;
		Debug.Log(Score);
	}
*/
