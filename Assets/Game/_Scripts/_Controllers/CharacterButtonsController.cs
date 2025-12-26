using UnityEngine;
using Zenject;

public class CharacterButtonsController : Controller
{
    private InputsSystem _inputsSystem;

    public CharacterButtonsController(Character character, InputsSystem inputsSystem) : base (character)
    {
        _inputsSystem = inputsSystem;
    }

    //[Inject]
    //private void Construct(Character character, InputsSystem inputsSystem)
    //{
    //    _character = character;
    //    _inputsSystem = inputsSystem;
    //}

    protected override void UpdateLogic()
    {
        Vector3 direction = Vector3.zero;

        if (_inputsSystem != null)
        {
            float horizontal = _inputsSystem.GetHorizontalInput();
            float vertical = _inputsSystem.GetVerticalInput();

            direction = new Vector3(horizontal, 0, vertical);
        }

        _character.SetDirection(direction);
    }
}