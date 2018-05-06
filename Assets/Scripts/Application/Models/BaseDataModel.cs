using SimpleJSON;

public abstract class BaseDataModel
{
    protected const string BASE_LANG = "fi";
    protected abstract void Load(JSONNode node);

    public static T Init<T>(JSONNode node) where T : BaseDataModel, new()
    {
        var item = new T();
        item.Load(node);
        return item;
    }
}