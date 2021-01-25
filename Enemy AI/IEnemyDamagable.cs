using UnityEngine;

public interface IEnemyDamagable
{
    void TakeDamage(float damage, GameObject inflictedBy);
}