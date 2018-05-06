static class YleAPIProgram
{
    public static YleAPI.YLERequest Program(this YleAPI api, int id)
    {
        var url = string.Format("programs/items{0}.json", id);
        return YleAPI.YLERequest.Init(api, null);
    }
}