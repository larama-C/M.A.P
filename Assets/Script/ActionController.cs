using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]

    Collider2D PlayerCollider;

    private float range = 1.0f;  // ������ ������ ������ �ִ� �Ÿ�

    private bool pickupActivated = false;  // ������ ���� �����ҽ� True 

    private RaycastHit2D hitInfo;  // �浹ü ���� ����

    [SerializeField]
    private LayerMask layerMask;  // Ư�� ���̾ ���� ������Ʈ�� ���ؼ��� ������ �� �־�� �Ѵ�.

    Color raycolor;

    public InventoryManager Inventory;


    private void Start()
    {
        PlayerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            CanPickUp();
        }
    }

    private void CheckItem()
    {
        hitInfo = Physics2D.Raycast(PlayerCollider.bounds.center, Vector2.down, PlayerCollider.bounds.extents.y + range, layerMask);
        if (hitInfo.collider != null)
        {
            raycolor = Color.black;
            if (hitInfo.transform.tag == "Item")
            {
                pickupActivated = true;
            }
        }
        else
        {
            raycolor = Color.white;
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
                Inventory.InputItem(hitInfo.transform.GetComponent<ItemPickUp>().item, hitInfo.transform.GetComponent<ItemPickUp>().item.ItemCount);
                Destroy(hitInfo.transform.gameObject);
                pickupActivated = false;
            }
        }
    }
}
