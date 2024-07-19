using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeypadController : MonoBehaviour
{
    public TextMeshProUGUI inputField; // TextMeshPro로 변경
    public string correctAnswer = "6971"; // 정답

    private string currentInput = "";

    void Start()
    {
        // 각 버튼에 이벤트 리스너를 추가합니다.
        for (int i = 0; i <= 9; i++)
        {
            string number = i.ToString();
            GameObject button = GameObject.Find("Button" + number);
            button.GetComponent<Button>().onClick.AddListener(() => OnNumberButtonClicked(number));
        }

        // Clear 버튼에 이벤트 리스너를 추가합니다.
        GameObject clearButton = GameObject.Find("ClearButton");
        clearButton.GetComponent<Button>().onClick.AddListener(ClearInput);

        // Enter 버튼에 이벤트 리스너를 추가합니다.
        GameObject enterButton = GameObject.Find("EnterButton");
        enterButton.GetComponent<Button>().onClick.AddListener(CheckInput);
    }

    void OnNumberButtonClicked(string number)
    {
        if (currentInput.Length < 4) // 최대 4자리 숫자만 입력받음
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
