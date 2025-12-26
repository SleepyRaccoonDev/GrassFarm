using UnityEngine;

public class NPCController : Controller
{
    private Vector3 _newPosition;

    public NPCController(Character character) : base (character)
    {
        _newPosition = character.transform.position;
    }

    protected override void UpdateLogic()
    {
        _newPosition.y = 0;
        Vector3 currentPosition = _character.transform.position;
        currentPosition.y = 0;

        Vector3 delta = _newPosition - currentPosition;

        delta.y = 0;

        if (delta.magnitude > 0.1f)
        {
            _character.SetDirection(delta);
        }
        else
        {
            _character.SetDirection(Vector3.zero);
        }
    }

    public override void SetNewPosition(Vector3 position) => _newPosition = position;
}