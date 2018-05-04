using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Networking
{
    
    public class WebRequest : UnityWebRequest
    {
        private static string BuildUri(params Dictionary<string, string>[] fields_params)
        {
            List<string> items = new List<string>();
            foreach (var fields in fields_params)
            {
                items.AddRange(fields.Select(
                    c => string.Format("{0}={1}", c.Key, WWW.EscapeURL(c.Value))
                ));
            }
            return "?" + string.Join("&", items.ToArray() );
        }

        public static UnityWebRequest Get(string uri, params Dictionary<string, string>[] fields)
        {
            uri += BuildUri(fields);
            UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "GET");
            unityWebRequest.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
            unityWebRequest.chunkedTransfer = false;

            unityWebRequest = new UnityWebRequest(uri, "GET", (DownloadHandler) new DownloadHandlerBuffer(),
                (UploadHandler) null);
            return unityWebRequest;
        }
    }
}