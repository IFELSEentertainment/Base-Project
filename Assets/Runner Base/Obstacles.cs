using UnityEngine;
using static MiniEnum;
public abstract class Obstacles : Collectable
{
    [SerializeField]
    private int damage;
    public int Damage => damage;
    
    [SerializeField]
    private DamageType damageType;
    public DamageType DamageType => damageType;
    public override void OnContact(GameObject _gameObject)
    {
        switch (damageType)
        {
            case DamageType.health:
                // Player.ChangeHealth.Invoke(-damage);
                break;
            case DamageType.score:
                // ScoreSystem.ScoreAdd.Invoke(-damage);
                break;
            default:
                Debug.LogError("ERROR!");
                break;
        }
    }
}
