using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WayPointUI : MonoSingleton<WayPointUI>
{
    public Image img;
    public Transform target;
    public TextMeshProUGUI text;
    public float distance;

    public bool isActive;
    public Vector3 offset;

    private void Update()
    {
        img.gameObject.SetActive(isActive);
        if(target == null || Camera.main == null) return;
        
        // 화면 경계값을 이미지 크기의 절반으로 설정
        float halfImgWidth = img.GetPixelAdjustedRect().width / 2f;
        float halfImgHeight = img.GetPixelAdjustedRect().height / 2f;
        
        float minX = halfImgWidth;
        float maxX = Screen.width - halfImgWidth;
        float minY = halfImgHeight;
        float maxY = Screen.height - halfImgHeight;

        // 타겟의 월드 좌표를 스크린 좌표로 변환
        Vector3 targetWorldPos = target.position + offset;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(targetWorldPos);

        // 타겟이 카메라 뒤에 있는지 확인
        bool isTargetBehind = screenPos.z < 0;
        
        if (isTargetBehind)
        {
            // 카메라에서 타겟으로의 방향 벡터 계산
            Vector3 directionToTarget = (targetWorldPos - Camera.main.transform.position).normalized;
            
            // 카메라의 로컬 좌표계로 방향 변환
            Vector3 localDirection = Camera.main.transform.InverseTransformDirection(directionToTarget);
            
            // 방향에 따라 화면 가장자리에 배치
            if (localDirection.x > 0)
            {
                screenPos.x = maxX; // 오른쪽 가장자리
            }
            else
            {
                screenPos.x = minX; // 왼쪽 가장자리
            }
            
            if (localDirection.y > 0)
            {
                screenPos.y = maxY; // 위쪽 가장자리
            }
            else
            {
                screenPos.y = minY; // 아래쪽 가장자리
            }
            
            // z값을 양수로 설정
            screenPos.z = 1f;
        }

        // 화면 경계 안으로 클램프
        screenPos.x = Mathf.Clamp(screenPos.x, minX, maxX);
        screenPos.y = Mathf.Clamp(screenPos.y, minY, maxY);
        
        img.transform.position = screenPos;
        
        // 플레이어와 타겟 사이의 거리 계산
        if (GameManager.Instance != null && GameManager.Instance.player != null)
        {
            Vector3 playerPosition = GameManager.Instance.player.transform.childCount > 1 
                ? GameManager.Instance.player.transform.GetChild(1).position 
                : GameManager.Instance.player.transform.position;
                
            distance = Vector3.Distance(playerPosition, target.position);
            text.text = $"{(int)distance}m";
        }

        // 거리에 따른 스케일과 투명도 조정
        if (distance < 5f)
        {
            img.transform.localScale = Vector3.one * 0.5f;
            img.color = new Color(1f, 1f, 1f, 0.5f);
        }
        else
        {
            img.transform.localScale = Vector3.one;
            img.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
