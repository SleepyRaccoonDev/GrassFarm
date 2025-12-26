using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class Grass : Item, ICuttable, IDroppable
{
    public event Action<Vector3> IsDropped;

    private const string AlphaMaterialKey = "_AlphaTreshold";
    private const string GrowMaterialKey = "_Grow";
    private const string ColorMaterialKey = "_MainColor";
    private readonly Color GrowColor = Color.black;
    private const float RespawnTime = 0.5f;
    private const float AlphaTargetValue = 1.2f;
    private const float MaxGrowValue = 1f;

    private Material _material;

    private float _speedCutting;
    private float _speedGrowing;

    private float _startAlphaValue;
    private Color _startColor;

    private bool _isCantBeCutted;

    private ParticleSystem _cutEffect;

    private YieldInstruction _waitForSeconds = new WaitForSeconds(RespawnTime);

    public override void Initialize(CuttableType cutableType, Color color, float currency)
    {
        Type = cutableType;

        _material = GetComponent<MeshRenderer>().material;

        _material.SetColor(ColorMaterialKey, color);

        _startAlphaValue = _material.GetFloat(AlphaMaterialKey);
        _startColor = _material.GetColor(ColorMaterialKey);

        _cutEffect = GetComponentInChildren<ParticleSystem>();
    }

    public void Cut()
    {
        if (_isCantBeCutted == true)
            return;

        IsDropped?.Invoke(transform.position);

        _isCantBeCutted = true;

        if (_cutEffect != null && UnityEngine.Random.value < .9f)
            _cutEffect.Play();

        StartCoroutine(CutAnimation());
    }

    [Inject]
    private void Construct(GameConfig gameConfig)
    {
        _speedCutting = gameConfig.SpeedDisappearing;
        _speedGrowing = gameConfig.SpeedGrowing;
    }

    private IEnumerator CutAnimation()
    {
        float targetValue = AlphaTargetValue;

        float currentValue = _material.GetFloat(AlphaMaterialKey);

        while (currentValue < targetValue)
        {
            currentValue += Time.deltaTime * _speedCutting;
            _material.SetFloat(AlphaMaterialKey, currentValue);
            yield return null;
        }

        _material.SetFloat(GrowMaterialKey, 0);
        _material.SetFloat(AlphaMaterialKey, _startAlphaValue);
        _material.SetColor(ColorMaterialKey, GrowColor);

        StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        float currentValue = _material.GetFloat(GrowMaterialKey);

        while (currentValue < MaxGrowValue)
        {
            currentValue += Time.deltaTime * _speedGrowing;

            _material.SetFloat(GrowMaterialKey, currentValue);

            yield return null;
        }

        yield return _waitForSeconds;

        _material.SetColor(ColorMaterialKey, _startColor);
        _isCantBeCutted = false;
    }
}