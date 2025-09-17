using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    public GameObject inventoryPanel;
   [SerializeField]
    public GameObject slotPrefab;
   [SerializeField]
    public int slotCount;
   [SerializeField]
    public GameObject[] itemPrefabs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
            if (i < itemsPrefabs.Length)
            {
                GameObject item = Instantiate(itemPrefabs[i], slot.transform);
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = item;
            }
        }
    }


}
