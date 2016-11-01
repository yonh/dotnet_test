using System;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;


namespace Demo
{

    class PostParams
    {
        private List<KeyValuePair<string,string>> postData = new List<KeyValuePair<string, string>>();

        public HttpContent Parameters
        {
            get { return new FormUrlEncodedContent(postData); }
        }

        public void Add(string key, string value)
        {
            postData.Add(new KeyValuePair<string, string>(key,value));
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            string uri = "http://192.168.50.229/niaoyunapi/api/";//API请求地址
            // String url = "http://api.niaoyun.com/";       //API请求地址

            PostParams pp = new PostParams();
            pp.Add("dataType", "json");
            pp.Add("dataType","json");//业务类型
            pp.Add("module","server");//业务类型
            pp.Add("action","restart");//接口名称
            pp.Add("userid","2");//代理商ID
            pp.Add("time","1477986314");//时间戳
            pp.Add("sign","cf4b57d4d066142fe9559c30b66e3aae");//权限令牌
            pp.Add("taskid","1477986314"); //任务编号
            pp.Add("notifyUrl", "http://demo.niaoyun.com/notify.php");//异步返回地址
            pp.Add("guid","1234"); //服务器唯一标识
            
            var client = new HttpClient();
            HttpResponseMessage resp = client.PostAsync(uri, pp.Parameters).Result;
            if (resp.StatusCode == HttpStatusCode.OK) {
                string json = resp.Content.ReadAsStringAsync().Result.ToString();
                Dictionary<string, string> result = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                if (result.ContainsKey("code") && result["code"] == "0") {
                    Console.WriteLine("重启成功");
                } else {
                    Console.WriteLine("重启失败,失败原因:"+result["message"]);
                }
            } else {
                Console.WriteLine("请求异常");
            }
        }
    }
}
