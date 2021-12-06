using UnityEngine;
using static MiniEnum;

public abstract class Collectable : Contactable
{
    [SerializeField]
    private int buffValue;
    [SerializeField] 
    private BuffType buffType;

    public BuffType BuffType => buffType;
    public int BuffValue => buffValue;
    
    public override void OnContact(GameObject _gameObject)
    {
        switch (buffType)
        {
            case BuffType.health:
                // Player.ChangeHealth.Invoke(buffValue);
                break;
            case BuffType.score:
                // ScoreSystem.ScoreAdd.Invoke(buffValue);
                break;
            default:
                Debug.LogError("ERROR!");
                break;
        }
    }
}