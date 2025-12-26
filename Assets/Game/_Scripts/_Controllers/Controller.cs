using UnityEngine;

public abstract class Controller
{
    protected Character _character;

    private bool _isEnable;

    public virtual void Enable() => _isEnable = true;

    public virtual void Disable() => _isEnable = false;

    public virtual Character GetCharacter() => _character;

    protected Controller(Character character)
    {
        _character = character;
    }

    public void OnUpdate()
    {
        if (_isEnable == false)
            return;

        UpdateLogic();
    }

    protected abstract void UpdateLogic();

    public virtual void SetNewPosition(Vector3 position)
    {

    }
}