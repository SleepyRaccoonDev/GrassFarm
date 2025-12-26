using System;
using System.Collections.Generic;

public class Cutter
{
    public event Action OnCut;

    public void Cut(List<ICuttable> cuttables, Backpack backpack)
    {
        bool isCutted = false;

        foreach (var cuttable in cuttables)
        {
            if (backpack.IsFull)
                return;

            cuttable.Cut();

            isCutted = true;
        }

        if (isCutted)
            OnCut?.Invoke();
    }
}