using Godot;
using System;
using System.Linq;
using Godot.Collections;

public partial class StateMachine : Node
{
    [Export] public NodePath initialState;
    
    private State _currentState;

    public override void _Ready()
    {
        // Adelanto la ejecucion del State Machine.
        ProcessPhysicsPriority = -1; 
        
        foreach (State s in GetChildrenStates(this)) {
            s.fsm = this;
            s.Exit(); // reset
        }

        _currentState = GetNode<State>(initialState);
        _currentState.Enter();
    }

    private Array<State> GetChildrenStates(Node node)
    {
        Array<State> states = new Array<State>();
        
        foreach (Node child in node.GetChildren())
        {
            if (child is State s)
            {
                states.Add(s);
                states.AddRange(GetChildrenStates(s));
            }
        }

        return states;
    }

    public override void _Process(double delta)
    {
        _currentState.Update(delta);
    }
    
    public override void _PhysicsProcess(double delta)
    {
        _currentState.PhysicsUpdate(delta);
    }
    
    public void TransitionTo(string key, Dictionary payload = null) {
        GD.Print(key);
        _currentState.Exit();
        _currentState = GetNode<State>(key);
        _currentState.Enter(payload);
    }
}