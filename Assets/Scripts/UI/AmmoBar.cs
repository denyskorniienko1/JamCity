using TMPro;
using UnityEngine;

public class AmmoBar : MonoBehaviour
{
    public TMP_Text MagazineText;
    public TMP_Text TotalText;
    public IntEventListener magazineEventListener;
    public IntEventListener totalEventListener;

    private WeaponSO currentWeapon;

    public void UpdateMagazine(int value)
    {
        MagazineText.text = value.ToString();
    }

    public void UpdateTotal(int value)
    {
        TotalText.text = value.ToString();
    }

    public void UpdateWeapon(WeaponSO weapon)
    {
        currentWeapon = weapon;
        magazineEventListener.Unsubscribe();
        totalEventListener.Unsubscribe();
        magazineEventListener.Event = weapon.MagazineCount.Event;
        totalEventListener.Event = weapon.TotalCount.Event;
        magazineEventListener.Subscribe();
        totalEventListener.Subscribe();

        MagazineText.text = currentWeapon.MagazineCount.RuntimeValue.ToString();
        TotalText.text = currentWeapon.TotalCount.RuntimeValue.ToString();
    }
}