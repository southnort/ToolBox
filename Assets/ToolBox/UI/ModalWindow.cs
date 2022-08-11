using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ModalWindow : UI_State
{
    public override void Show()
    {
        PlayTapSound();
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }

}
