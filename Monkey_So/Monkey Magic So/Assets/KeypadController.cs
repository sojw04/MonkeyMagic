using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeypadController : MonoBehaviour
{
    public TextMeshProUGUI inputField; // TextMeshPro�� ����
    public string correctAnswer = "6971"; // ����

    private string currentInput = "";

    void Start()
    {
        // �� ��ư�� �̺�Ʈ �����ʸ� �߰��մϴ�.
        for (int i = 0; i <= 9; i++)
        {
            string number = i.ToString();
            GameObject button = GameObject.Find("Button" + number);
            button.GetComponent<Button>().onClick.AddListener(() => OnNumberButtonClicked(number));
        }

        // Clear ��ư�� �̺�Ʈ �����ʸ� �߰��մϴ�.
        GameObject clearButton = GameObject.Find("ClearButton");
        clearButton.GetComponent<Button>().onClick.AddListener(ClearInput);

        // Enter ��ư�� �̺�Ʈ �����ʸ� �߰��մϴ�.
        GameObject enterButton = GameObject.Find("EnterButton");
        enterButton.GetComponent<Button>().onClick.AddListener(CheckInput);
    }

    void OnNumberButtonClicked(string number)
    {
        if (currentInput.Length < 4) // �ִ� 4�ڸ� ���ڸ� �Է¹���
        {
            currentInput += number;
            inputField.text = currentInput;
        }
    }

    public void ClearInput()
    {
        currentInput = "";
        inputField.text = currentInput;
    }

    public void CheckInput()
    {
        if (currentInput == correctAnswer)
        {
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Incorrect!");
        }
    }
}
