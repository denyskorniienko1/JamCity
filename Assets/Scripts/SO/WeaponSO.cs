using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "SO/Weapon")]
public class WeaponSO : ScriptableObject
{
	public IntObservableVariableSO MagazineCount;
	public IntObservableVariableSO TotalCount;
	public float MuzzleVelocity = 700f;
	public float CooldownWindow = 0.1f;

	public int BulletPoolDefaultCapacity = 90;
	public int BulletPoolMaxSize = 180;
}