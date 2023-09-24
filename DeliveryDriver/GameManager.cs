using Godot;
using System;

public partial class GameManager : Node
{
	public int Score = 0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void AddPoints()
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