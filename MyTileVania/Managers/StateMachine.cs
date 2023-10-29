using Godot;
using System;
using System.Collections.Generic;

public partial class StateMachine : Node
{
    [Export] public NodePath initialState;
    
    private State _currentState;

    public State GetState() => _currentState;
    
    public override void _Ready()
    {
        foreach (Node node in GetChildren()) {
            if (node is State s) {
                s.fsm = this;
                s.Exit(); // reset
            }
        }

        GD.Print(initialState);
        _currentState = GetNode<State>(initialState);
        GD.Print(_currentState.Name);
        _currentState.Enter();
    }

    public override void _Process(double delta)
    {
        _currentState.Update((float)delta);
    }
    
    public override void _PhysicsProcess(double delta)
    {
        _currentState.PhysicsUpdate((float)delta);
    }
    
    /*
    public override void _UnhandledInput(InputEvent @event)
    {
        _currentState.HandleInput(@event);
        @event.Dispose();
    }*/
    
    public void TransitionTo(string key) {
        GD.Print(_currentState.Name);
        _currentState.Exit();
        GD.Print(key);
        _currentState = GetNode<State>(key);
        GD.Print(_currentState.Name);
        _currentState.Enter();
    }
}