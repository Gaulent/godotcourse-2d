using Godot;

[GlobalClass]
public partial class QuestionRes : Resource
{
    [Export] public string question;
    [Export] public string[] answers;
    [Export] public int rightAnswer;
}