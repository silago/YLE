using Networking;

namespace YLE.API
{
    internal static class YleAPIProgram
    {
        public static YLERequest Program(this YleAPI api, int id)
        {
            var url = string.Format("programs/items{0}.json", id);
            return YLERequest.Init(api, url);
        }
    }
}