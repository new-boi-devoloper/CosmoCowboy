using Cysharp.Threading.Tasks;
using EnemyScripts.EnemyManagement;
using UnityEngine;

namespace EnemyScripts.EnemyInterfaces
{
    public interface IEnemyInformant
    {
        public void InformOtherEnemies(Vector3 position, float radius);
        UniTask MoveToPositionAndReturn(EnemyBase enemy, Vector3 position);
    }
}