using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PickupType { Health, Ammo, GoldCoin, SilverCoin }

    [SerializeField] private PickupType type;
    [SerializeField] private int value = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    private void Collect()
    {
        switch (type)
        {
            case PickupType.Health:
                GameManager.Instance.PlayerHeal(value);
                break;
            case PickupType.Ammo:
                GameManager.Instance.AddAmmo(value);
                break;
            case PickupType.GoldCoin:
                GameManager.Instance.AddPoints(value);
                break;
            case PickupType.SilverCoin:
                GameManager.Instance.AddPoints(value);
                break;
        }
        Destroy(gameObject);
    }
}