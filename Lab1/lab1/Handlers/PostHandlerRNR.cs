using lab1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Script.Serialization;
namespace lab1.Handlers
{
    public class PostHandlerRNR : IHttpHandler
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            HttpRequest req = context.Request;
            HttpResponse res = context.Response;

            res.ContentType = "application/json";

            int number;

            if (int.TryParse(req.Params["result"], out number))
            {
                try { 
                Result.result = number;
                int top = Result.stack.Peek();
                res.Write(js.Serialize(new { result = Result.result + top, stack = Result.stack }));
                Result.stack.Push(number);
                } catch (InvalidOperationException) {
                res.Write(js.Serialize(new { result = Result.result, stack = "Stack is empty" }));
                }
            }
            else
            {
                res.Write(js.Serialize(new { error = new { message = "Type of Params['result'] is not Integer",result = req.Params["result"] } }));
            }
        }
    }
}