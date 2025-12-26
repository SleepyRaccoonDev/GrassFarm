using System;
using Zenject;

public class Wallet
{
    public event Action<float> IsGreenCoin;
    public event Action<float> IsYellowCoin;

    public float GreenCoins { get; private set; }
    public float YellowCoins { get; private set; }

    [Inject]
    private void Construct(Backpack backpack)
    {
        backpack.IsSelled += AddCoin;
    }

    public void AddCoin(CuttableType type, float value)
    {
        switch (type)
        {
            case CuttableType.Green:
                ChangeGreenCoins(value);
                break;

            default:
            case CuttableType.Yellow:
                ChangeYellowCoins(value);
                break;
        }
    }

    public bool TryBuy(float greenValue, float yellowValue)
    {
        bool value = (GreenCoins - greenValue >= 0) && (YellowCoins - yellowValue) >= 0;

        if (value)
        {
            ChangeGreenCoins(-greenValue);
            ChangeYellowCoins(-yellowValue);
        }

        return value;
    }

    private void ChangeGreenCoins(float value)
    {
        if (GreenCoins + value < 0)
            GreenCoins = 0;

        GreenCoins += value;
        ChangeCoinsValue(IsGreenCoin, GreenCoins);
    }

    private void ChangeYellowCoins(float value)
    {
        if (YellowCoins + value < 0)
            YellowCoins = 0;

        YellowCoins += value;
        ChangeCoinsValue(IsYellowCoin, YellowCoins);
    }

    private void ChangeCoinsValue(Action<float> action, float value)
    {
        action?.Invoke(value);
    }
}