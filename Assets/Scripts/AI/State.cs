using UnityEngine;

public abstract class State : ScriptableObject
{
    public bool IsFinished { get; protected set; }
    [HideInInspector] public Character character;

    public virtual void Init(){ }

    public abstract void Run();
}
