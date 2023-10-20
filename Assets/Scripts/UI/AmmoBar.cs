using TMPro;
using UnityEngine;

public class AmmoBar : MonoBehaviour
{
    public TMP_Text MagazineText;
    public TMP_Text TotalText;

    public void UpdateMagazine(int value)
    {
        MagazineText.text = value.ToString();
    }

    public void UpdateTotal(int value)
    {
        TotalText.text = value.ToString();
    }
}