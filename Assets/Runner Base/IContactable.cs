using UnityEngine;

interface IContactable
{
    void Effect(GameObject _gameObject);
    void OnContact(GameObject _gameObject);
    bool Requir(GameObject _gameObject);
}
