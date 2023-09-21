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

    private float range = 1.0f;  // ������ ������ ������ �ִ� �Ÿ�

    private bool pickupActivated = false;  // ������ ���� �����ҽ� True 

    private RaycastHit2D hitInfo;  // �浹ü ���� ����

    [SerializeField]
    private LayerMask layerMask;  // Ư�� ���̾ ���� ������Ʈ�� ���ؼ��� ������ �� �־�� �Ѵ�.

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
