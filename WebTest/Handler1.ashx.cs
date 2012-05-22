using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SDN2.VO;
using System.Threading.Tasks;
using System.Threading;
using SDN2.Common;

namespace WebTest
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //GObject go = new GObject();
            //go["name"] = "小小公主";
            //context.Response.Write(go.GetJSON(SDN2.Common.Web.JSONEncodingType.Chniese));
            Parallel.For(10,0,i=>{
                DoWork();
            });
           
        }



        private void DoWork()
        {
            Thread.Sleep(1000);
            HttpContext.Current.Response.Write(DateTime.Now.ToString("HH:mm:ss sss"));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }


        }
    }
}