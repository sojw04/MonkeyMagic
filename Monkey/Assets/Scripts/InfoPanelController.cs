using UnityEngine;

public class InfoPanelController : MonoBehaviour
{
    [SerializeField] private GameObject infoPanel;

    private void Start()
    {
        if (infoPanel == null)
        {
            Debug.LogError("InfoPanel is not assigned.");
        }
        else
        {
            infoPanel.SetActive(false); // 게임 시작 시 InfoPanel을 비활성화
        }
    }

    private void Update()
    {
        if (infoPanel != null && infoPanel.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            infoPanel.SetActive(false);
        }
    }
}
