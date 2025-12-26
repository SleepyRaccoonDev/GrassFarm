using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

public class UIController : MonoBehaviour
{
    private const float SpeedOfTextChange = 10f;

    [SerializeField] private TextMeshProUGUI _greenCoinsText;
    [SerializeField] private TextMeshProUGUI _yellowCoinsText;

    private Coroutine _coroutineGreen;
    private Coroutine _coroutineYellow;

    private Wallet _wallet;

    [Inject]
    private void Construct(Wallet wallet)
    {
        _wallet = wallet;

        wallet.IsGreenCoin += ChangeGreenCoinsValue;
        wallet.IsYellowCoin += ChangeYellowCoinsValue;
    }

    private void OnDisable()
    {
        _wallet.IsGreenCoin -= ChangeGreenCoinsValue;
        _wallet.IsYellowCoin -= ChangeYellowCoinsValue;
    }

    private void ChangeGreenCoinsValue(float value)
    {
        if (_coroutineGreen != null)
            StopCoroutine(_coroutineGreen);

        _coroutineGreen = StartCoroutine(LerpChangeValue(_greenCoinsText, value));
    }

    private void ChangeYellowCoinsValue(float value)
    {
        if (_coroutineYellow != null)
            StopCoroutine(_coroutineYellow);

        _coroutineYellow = StartCoroutine(LerpChangeValue(_yellowCoinsText, value));
    }

    private IEnumerator LerpChangeValue(TextMeshProUGUI textMeshPro, float targetValue)
    {
        yield return null;

        if(float.TryParse(textMeshPro.text, out float result))
        {
            while (result != targetValue)
            {
                result = Mathf.Lerp(result, targetValue, SpeedOfTextChange * Time.deltaTime);
                textMeshPro.SetText($"{result:0}");
                yield return null;
            }
        }

        textMeshPro.SetText($"{targetValue:0}");
    }
}