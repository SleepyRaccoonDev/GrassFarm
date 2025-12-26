using UnityEngine;

public class InputsSystem 
{
    private const string HorizontalInputName = "Horizontal";
    private const string VerticalinputName = "Vertical";

    public float GetHorizontalInput()
    {
        return Input.GetAxisRaw(HorizontalInputName);
    }

    public float GetVerticalInput()
    {
        return Input.GetAxisRaw(VerticalinputName);
    }

    public bool IsUseButtonPressed()
    {
        return Input.GetKeyDown(KeyCode.F);
    }
}