using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]

    CapsuleCollider2D PlayerCollider;

    private float range = 1.0f;  // 아이템 습득이 가능한 최대 거리

    private bool pickupActivated = false;  // 아이템 습득 가능할시 True 

    private RaycastHit2D hitInfo;  // 충돌체 정보 저장

    [SerializeField]
    private LayerMask layerMask;  // 특정 레이어를 가진 오브젝트에 대해서만 습득할 수 있어야 한다.

    Color raycolor;

    public InventoryManager Inventory;

    void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CanPickUp();
        }
    }

    private void CheckItem()
    {
        hitInfo = Physics2D.Raycast(PlayerCollider.bounds.center, Vector2.down, PlayerCollider.bounds.extents.y + range, layerMask);
        if (hitInfo.collider != null)
        {
            raycolor = Color.green;
            if (hitInfo.transform.tag == "Item")
            {
                pickupActivated = true;
            }
        }
        else
        {
            raycolor = Color.red;
            pickupActivated = false;
        }
        Debug.DrawRay(PlayerCollider.bounds.center, Vector2.down * (PlayerCollider.bounds.extents.y + range), raycolor);
    }

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hitInfo.transform != null)
            {
                Inventory.InputItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                Destroy(hitInfo.transform.gameObject);
                pickupActivated = false;
            }
        }
    }
}
