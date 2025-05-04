using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float Health;
    public float speed;
}

public class Spawner : MonoBehaviour
{
    public float spawnInterval = 1.2f; // 소환 주기
    public float spawnRange = 5f; // 플레이어 주변 범위
    public int spawnCount = 1; // 한 번에 몇 마리 소환

    float timer = 0f;
    float elapsedTime = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        elapsedTime += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;

            for (int i = 0; i < spawnCount; i++)
            {
                SpawnEnemy();
            }
        }

        // 20초마다 소환 속도 빨라지고 수량 증가
        if (elapsedTime >= 20f)
        {
            spawnInterval = Mathf.Max(0.5f, spawnInterval - 0.1f);
            spawnCount += 1;
            elapsedTime = 0f;
        }
    }

    void SpawnEnemy()
    {
        if (Gamemanager.instance.pool.Enemy_Frefabs == null || Gamemanager.instance.pool.Enemy_Frefabs.Length == 0)
        {
            Debug.LogWarning("Spawner: Enemy_Frefabs 배열이 비어있습니다!");
            return;
        }

        int maxIndex = Gamemanager.instance.pool.Enemy_Frefabs.Length;
        int randomIndex = Random.Range(0, maxIndex);

        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        Vector3 playerPos = player.transform.position;
        Vector3 randomOffset = new Vector3(
            Random.Range(-spawnRange, spawnRange),
            Random.Range(-spawnRange, spawnRange),
            0f
        );

        GameObject obj = Gamemanager.instance.pool.Get(randomIndex);
        if (obj == null) return;

        obj.transform.position = playerPos + randomOffset;
        obj.SetActive(true);

        Enemy enemy = obj.GetComponent<Enemy>();
        if (enemy != null)
        {
            // Animcom 유효성 검사
            if (enemy.Animcom == null || enemy.Animcom.Length == 0)
            {
                Debug.LogError($"[Spawner] enemy.Animcom 배열이 비어 있음! Init 생략");
                return;
            }

            // ⚠️ spriteType을 Animcom 범위 내에서 설정
            int spriteType = Random.Range(0, enemy.Animcom.Length);

            SpawnData spawnData = new SpawnData();
            spawnData.spriteType = spriteType;
            
            switch (spriteType)
            {
                case 0: // Zombie
                case 1: // R.Zombie
                    spawnData.Health = 20f;
                    spawnData.speed = 3f;
                    break;
                case 2: // Ent
                    spawnData.Health = 100f;
                    spawnData.speed = 1f;
                    break;
                case 3: // Goblin
                    spawnData.Health = 10f;
                    spawnData.speed = 6f;
                    break;
                case 4: // Skeleton
                    spawnData.Health = 5f;
                    spawnData.speed = 3f;
                    break;
            }

            enemy.Init(spawnData);
        }
        else
        {
            Debug.LogWarning($"[Spawner] Enemy 스크립트를 못 찾음! randomIndex = {randomIndex}");
        }
    }

}
