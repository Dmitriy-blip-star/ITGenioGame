using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestHP : MonoBehaviour
{
    public Image lineBar;

    public void MinusHP()
    {
        lineBar.fillAmount = CharacterController2D.health / 100;
    }
}
