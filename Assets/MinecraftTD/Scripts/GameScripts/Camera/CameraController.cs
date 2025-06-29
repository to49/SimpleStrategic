using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Основная цель")]
    public Transform playerTarget;  // Перетащите сюда игрока вручную

    [Header("Настройки")]
    public float followSpeed = 5f;
    public string backupTag = "Friendly";  // Тег для резервных целей

    private Vector3 _offset;

    void Start()
    {
        if (playerTarget != null)
        {
            _offset = transform.position - playerTarget.position;
            _offset.z = 0;
        }
    }

    void LateUpdate()
    {
        // Если игрок уничтожен, ищем резервную цель
        if (playerTarget == null)
        {
            FindBackupTarget();
            if (playerTarget == null) return;  // Если вообще нет целей
        }

        // Плавное движение камеры
        Vector3 targetPos = playerTarget.position + _offset;
        targetPos.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
    }

    void FindBackupTarget()
    {
        // Ищем все активные объекты с резервным тегом
        GameObject[] backups = GameObject.FindGameObjectsWithTag(backupTag);
        Transform closest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject obj in backups)
        {
            if (!obj.activeInHierarchy) continue;

            float dist = Vector2.Distance(transform.position, obj.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = obj.transform;
            }
        }

        // Переключаемся на новую цель
        if (closest != null)
        {
            playerTarget = closest;
            _offset = transform.position - playerTarget.position;
            _offset.z = 0;
        }
    }
}