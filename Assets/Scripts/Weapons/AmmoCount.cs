using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCount : MonoBehaviour
{
    [SerializeField] private TMP_Text ammo;
    [SerializeField] private TMP_Text mag;

    public static AmmoCount AmmoCountInstance;

    private void Awake()
    {
        AmmoCountInstance= this;
    }

    public void UpdateAmmoText(int presentAmmo)
    {
        ammo.text = "Ammo : " + presentAmmo;
    }
    public void UpdateMagText(int presentMag)
    {
        mag.text = "Magazines : " + presentMag;
    }
}
