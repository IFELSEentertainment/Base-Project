using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Contactable : MonoBehaviour, IContactable
{
    protected Collider baseCollider;
    
    [SerializeField] 
    private Enum_Particles _particle;
    [SerializeField] 
    private bool destory = false;
    public bool Destory => destory;


    [ShowIf("destory")]
    [SerializeField] private float delayDestory = 0;
    public float DelayDestory => delayDestory;
    protected virtual void Awake()
    {
        baseCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!Requir(other.gameObject)) return;
        OnContact(other.gameObject);
        Effect(other.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(!Requir(other.gameObject)) return;
        OnContact(other.gameObject);
        Effect(other.gameObject);
    }

    public virtual void Effect(GameObject _gameObject)
    {
        // ParticleSpawnSystem.instance.SpawnParticle(_particle,transform.position,Quaternion.identity);
        if (destory)
        {
            Destroy(gameObject,delayDestory);
        }
    }
    public abstract void OnContact(GameObject _gameObject);
    public abstract bool Requir(GameObject _gameObject);
}
