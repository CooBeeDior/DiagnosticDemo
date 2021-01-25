using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.Extensions.DiagnosticAdapter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiagnosticDemo
{
    public class MyObserver<T> : IObserver<T>
    {
        private Action<T> _next;
        public MyObserver(Action<T> next)
        {
            _next = next;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(T value) => _next(value);
    }



    public class MyDiagnosticListener
    {
        //发布的消息主题名称
        [DiagnosticName("MyTedawdwst1.Log")]
        //发布的消息参数名称和发布的属性名称要一致
        public void MyLog(string name, string address)
        {
            System.Console.WriteLine($"监听名称:MyTest.Log");
            System.Console.WriteLine($"获取的信息为:{name}的地址是{address}");
        }
    }

    public class HttpClientTracingDiagnosticProcessor
    {
        public string ListenerName { get; } = "HttpHandlerDiagnosticListener";

        [DiagnosticName("System.Net.Http.Request")]
        public void HttpRequest(HttpRequestMessage request)
        {
        }

        [DiagnosticName("System.Net.Http.Response")]
        public void HttpResponse(HttpResponseMessage response)
        {
        }

        [DiagnosticName("System.Net.Http.Exception")]
        public void HttpException(HttpRequestMessage request, Exception exception)
        {
        }
    }

    public class HostingTracingDiagnosticProcessor
    {
        public string ListenerName { get; } = "Microsoft.AspNetCore";

        [DiagnosticName("Microsoft.AspNetCore.Hosting.BeginRequest")]
        public void BeginRequest(HttpContext httpContext)
        {
        }

        [DiagnosticName("Microsoft.AspNetCore.Hosting.EndRequest")]
        public void EndRequest(HttpContext httpContext)
        {
        }

        [DiagnosticName("Microsoft.AspNetCore.Diagnostics.UnhandledException")]
        public void DiagnosticUnhandledException(HttpContext httpContext, Exception exception)
        {
        }

        [DiagnosticName("Microsoft.AspNetCore.Hosting.UnhandledException")]
        public void HostingUnhandledException(HttpContext httpContext, Exception exception)
        {
        }

        //[DiagnosticName("Microsoft.AspNetCore.Mvc.BeforeAction")]
        public void BeforeAction(ActionDescriptor actionDescriptor, HttpContext httpContext)
        {
        }

        //[DiagnosticName("Microsoft.AspNetCore.Mvc.AfterAction")]
        public void AfterAction(ActionDescriptor actionDescriptor, HttpContext httpContext)
        {
        }
    }


    class Program
    {
        static async Task Main(string[] args)
        {

            //AllListeners获取所有发布者，Subscribe为发布者注册订阅者MyObserver
            DiagnosticListener.AllListeners.Subscribe(new MyObserver<DiagnosticListener>(listener =>
            {
                ////判断发布者的名字
                //if (listener.Name == "MyTest")
                //{
                //    ///**重要** 注:1 通过常规对象订阅
                //    listener.Subscribe(new MyObserver<KeyValuePair<string, object>>(listenerData =>
                //    {
                //        System.Console.WriteLine($"监听名称:{listenerData.Key}");
                //        dynamic data = listenerData.Value;
                //        //打印发布的消息
                //        System.Console.WriteLine($"获取的信息为:{data.Name}的地址是{data.Address}");
                //    }));

                //    //listener.SubscribeWithAdapter(new MyDiagnosticListener());
                //}

                //if (listener.Name == "MyTest1")
                //{
                //    listener.SubscribeWithAdapter(new MyDiagnosticListener());


                //} 

                //if (listener.Name == "HttpHandlerDiagnosticListener")
                //{
                //    ///**重要**  注：2通过适配器模式去订阅
                //    listener.SubscribeWithAdapter(new HttpClientTracingDiagnosticProcessor());
                //}


                //判断发布者的名字
                if (listener.Name == "HttpHandlerDiagnosticListener")
                {
                    ///**重要** 注:1 通过常规对象订阅
                    listener.Subscribe(new MyObserver<KeyValuePair<string, object>>(listenerData =>
                   {
                       var data = listenerData;
                   }));

                    //listener.SubscribeWithAdapter(new MyDiagnosticListener());
                }

            }));


            HttpClient client = new HttpClient();
            var resp = await client.GetAsync("http://www.baidu.com");

            var result = await resp.Content.ReadAsStringAsync();


            //{      //声明DiagnosticListener并命名为MyTest
            //    DiagnosticSource diagnosticSource = new DiagnosticListener("MyTest");
            //    string pubName = "MyTest.Log";
            //    //判断是否存在MyTest.Log的订阅者
            //    if (diagnosticSource.IsEnabled(pubName))
            //    {
            //        //发送名为MyTest.Log的消息，包含Name，Address两个属性
            //        diagnosticSource.Write(pubName, new { Name = "old王", Address = "隔壁" });
            //    }
            //}


            //{
            //    //声明DiagnosticListener并命名为MyTest
            //    DiagnosticSource diagnosticSource1 = new DiagnosticListener("MyTest1");
            //    string pubName1 = "MyTedawdwst1.Log";
            //    //判断是否存在MyTest.Log的订阅者
            //    if (diagnosticSource1.IsEnabled(pubName1))
            //    {
            //        //发送名为MyTest.Log的消息，包含Name，Address两个属性
            //        diagnosticSource1.Write(pubName1, new { Name = "old王", Address = "隔壁" });
            //    }

            //}







            Console.ReadLine();
        }
    }
}
