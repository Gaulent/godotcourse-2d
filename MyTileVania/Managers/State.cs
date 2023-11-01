using Godot;
using System;
using Godot.Collections;

public partial class State : Node
{
    public StateMachine fsm;
	
    public virtual void Enter(Dictionary payload = null) {}
    public virtual void Exit() {}
    public virtual void Update(double delta) {}
    public virtual void PhysicsUpdate(double delta) {}
}