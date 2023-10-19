using Assets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject StatusWindow;
    public StatusManager statusManager;
    public GameObject InventoryWindow;
    public InventoryManager inventoryManager;
    public GameObject EquipmentWindow;
    public EquipmentManager equipmentManager;

    // Start is called before the first frame update
    void Start()
    {
        statusManager = StatusWindow.GetComponent<StatusManager>();
        inventoryManager = InventoryWindow.GetComponent<InventoryManager>();
        equipmentManager = EquipmentWindow.GetComponent<EquipmentManager>();
    }

    private void Awake()
    {
        Invoke("Init", 0.0001f); // 2초뒤 LaunchProjectile함수 호출
    }

    void Init()
    {
        StatusWindow.SetActive(false);
        InventoryWindow.SetActive(false);
        EquipmentWindow.SetActive(false);
    }

    void InputSystem()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if(StatusWindow.activeSelf == false)
            {
                StatusWindow.SetActive(true);
            }
            else
            {
                StatusWindow.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (InventoryWindow.activeSelf == false)
            {
                InventoryWindow.SetActive(true);
            }
            else
            {
                InventoryWindow.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (EquipmentWindow.activeSelf == false)
            {
                EquipmentWindow.SetActive(true);
            }
            else
            {
                EquipmentWindow.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        InputSystem();
    }
}
