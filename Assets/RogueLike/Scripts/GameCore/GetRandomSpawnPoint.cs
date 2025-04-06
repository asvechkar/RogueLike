using UnityEngine;

namespace RogueLike.Scripts.GameCore
{
    public class GetRandomSpawnPoint
    {
        public Vector3 GetRandomPoint(Transform minPos, Transform maxPos)
        {
            var spawnPoint = Vector3.zero;
            var verticalSpawn = Random.Range(0f, 1f) > 0.5f;

            if (verticalSpawn)
            {
                spawnPoint.y = Random.Range(minPos.position.y, maxPos.position.y);
                spawnPoint.x = Random.Range(0f, 1f) > 0.5f ? minPos.position.x : maxPos.position.x;
            }
            else
            {
                spawnPoint.y = Random.Range(minPos.position.y, maxPos.position.y);
                spawnPoint.x = Random.Range(0f, 1f) > 0.5f ? minPos.position.x : maxPos.position.x;
            }
            
            return spawnPoint;
        }
    }
}