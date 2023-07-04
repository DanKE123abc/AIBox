using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AIBox.ChatApi
{
    public class ChatApi
    {
        private const string api = "https://fd.52ai.pw/v1/chat/completions";
        //private const string api = "https://service-d20dvkuc-1318437443.hk.apigw.tencentcs.com/v1/chat/completions";
        private const string model = "gpt-3.5-turbo";
        private const string api_key = "Bearer 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";
        List<Dictionary<string, string>> messages = new List<Dictionary<string, string>>
    {
        new Dictionary<string, string>() { { "role", "user" }, { "content", "" } }
    };

        private string AskChatGpt(List<Dictionary<string, string>> messages)
        {
            var data = new Dictionary<string, object>
        {
            { "model", model },
            { "messages", messages },
            { "stream", false }
        };
            var json = JsonMapper.ToJson(data);

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", api_key);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = client.PostAsync(api, content).Result;
            var responseJson = response.Content.ReadAsStringAsync().Result;

            //Debug.Log(responseJson);
            var responseDict = JsonMapper.ToObject(responseJson);
            return responseDict["choices"][0]["message"]["content"].ToString();
        }

        public string Get(string content)
        {
            try
            {
                var d = new Dictionary<string, string>() { { "role", "user" }, { "content", content } };
                messages.Add(d);

                content = AskChatGpt(messages);

                d = new Dictionary<string, string>() { { "role", "assistant" }, { "content", content } };
                messages.Add(d);
                return "" + content;
            }
            catch (Exception ex)
            {
                messages.RemoveAt(messages.Count - 1);
                return "出现错误，请联系开发者" + ex + "\n Github链接：https://github.com/DanKE123abc/AIBox";
            }
        }
    }
}
