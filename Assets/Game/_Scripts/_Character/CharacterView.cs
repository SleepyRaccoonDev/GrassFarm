using UnityEngine;

public class CharacterView : MonoBehaviour
{
    private readonly int HorizontalKey = Animator.StringToHash("Horizontal");
    private readonly int VerticalKey = Animator.StringToHash("Vertical");
    private readonly string BaseLayerKey = "Base";
    private readonly string MowLayerKey = "Mow";

    private Character _character;
    private Animator _animator;

    private int _baseIndex;
    private int _mowIndex;

    

    private void Awake()
    {
        _character = GetComponentInParent<Character>();
        _animator = GetComponent<Animator>();

        _baseIndex = _animator.GetLayerIndex(BaseLayerKey);
        _mowIndex = _animator.GetLayerIndex(MowLayerKey);

        _character.IsCutted += RunCutAnimation;
    }

    private void OnDisable()
    {
        _character.IsCutted -= RunCutAnimation;
    }

    private void Update()
    {
        _animator.SetFloat(HorizontalKey, _character.CurrentDirection.x);
        _animator.SetFloat(VerticalKey, _character.CurrentDirection.z);
    }

    private void RunCutAnimation()
    {
        _animator.SetLayerWeight(_baseIndex, 0);
        _animator.SetLayerWeight(_mowIndex, 1);
    }

    public void StopCutAnimation()
    {
        _animator.SetLayerWeight(_mowIndex, 0);
        _animator.SetLayerWeight(_baseIndex, 1);
    }
}