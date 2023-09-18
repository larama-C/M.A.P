using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovableUI : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [SerializeField]
    private Transform _targetTr; // 이동될 UI

    private Vector2 _startingPoint;
    private Vector2 _moveBegin;
    private Vector2 _moveOffset;

    private void Awake()
    {
        // 이동 대상 UI를 지정하지 않은 경우, 자동으로 부모로 초기화
        if (_targetTr == null)
            _targetTr = transform.parent;
    }

    // 드래그 시작 위치 지정
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        _startingPoint = _targetTr.position;
        _moveBegin = eventData.position;
    }

    // 드래그 : 마우스 커서 위치로 이동
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        _moveOffset = eventData.position - _moveBegin;
        _targetTr.position = _startingPoint + _moveOffset;
    }
}
