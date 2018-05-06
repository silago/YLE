using Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoView : MonoBehaviour
{
    [SerializeField] private Text ItemTitle;
    [SerializeField] private Text Description;
    [SerializeField] private Text Title;

    public void Fill(IStringIndexed item)
    {
        Title.text = item["title"];
        Description.text = item["description"];
        ItemTitle.text = item["itemTitle"];
    }
}