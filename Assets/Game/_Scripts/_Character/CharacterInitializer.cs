
public class CharacterInitializer
{
    public Controller Initialize(Character character, Controller controller, float moveSpeed, float rorateSpeed)
    {
        Mover mover = new Mover(character.Rigidbody);
        Rotator rotator = new Rotator(character.Rigidbody);

        character.Initialize(mover, rotator, moveSpeed, rorateSpeed);

        controller.Enable();

        return controller;
    }
}