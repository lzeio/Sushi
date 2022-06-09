
public abstract class ZombieBaseState
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchStates();

    public abstract void InitializeSubState();


    void UpdateStates() { }

    void SwitchState() { }

    void SetSuperState() { }

    void SetSubState() { }
}
