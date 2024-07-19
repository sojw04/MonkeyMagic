using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class ObjDrag : MonoBehaviour
{
    private Camera cam;
    private Vector3 dragOffset; // 클릭했을때, object의 중앙좌표와과 클릭한 좌표사이의 차이
    Rigidbody2D rb;
    CapsuleCollider2D col;
    Compare compare;

    void Start()
    {
        //카메라를 이렇게 따로 지정해주는 이유 = 찾아놓고 계속 쓰기 위해(코드가 가벼워짐)
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        compare = GameObject.Find("ScaleController").GetComponent<Compare>();
        
    }


    void OnMouseDown()
    {
        dragOffset = transform.position - GetMousePos();


        rb.bodyType = RigidbodyType2D.Static;
        col.enabled = false;
    }

    void OnMouseDrag()
    {
        transform.position = GetMousePos() + dragOffset;
    }

    private void OnMouseUp()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        col.enabled = true;
        StartCoroutine(ExecuteAfterTime(0.5f));
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        // 지정된 시간 동안 대기
        yield return new WaitForSeconds(time);

        // 시간 경과 후 실행할 함수 호출
        compare.CompareWeight();
    }

    Vector3 GetMousePos()
    {
        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        // 마우스의 위치값 가져오기
        mousePos.z = 0;
        return mousePos; // 마우스 위치값 반환 
    }
}
