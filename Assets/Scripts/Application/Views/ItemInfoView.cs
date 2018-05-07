using Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoView : MonoBehaviour
{
    [SerializeField] private Text ItemTitle;
    [SerializeField] private Text Description;
    [SerializeField] private Text Title;
    [SerializeField] private Text Creator;
    [SerializeField] private Text Country;

    public void Fill(IStringIndexed item)
    {
        Title.text = item["title"];
        Description.text = item["description"];
        ItemTitle.text = item["itemTitle"];
        Creator.text = item["creator"];
        Country.text = item["country"];
    }
}