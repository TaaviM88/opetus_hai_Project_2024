using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboMultiplierPickable : PickUp, IPickable
{
    public int multiplierIncrease = 1; // The amount to increase the combo multip

    public void PickUp()
    {
        // Increase the player's combo multiplier
        UIManager.Instance.IncreaseComboMultiplier(multiplierIncrease);

        // Destroy or deactivate the pickable object
        Destroy(gameObject);
    }

}
