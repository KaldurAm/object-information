using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Newtonsoft.Json;
using ObjectInformation.DAL.Model;

namespace ObjectInformation.Models
{
    public static class Helper
    {
        public static IHtmlString CustomAjaxActionLink(this AjaxHelper ajaxHelper, AjaxOptions ajaxOptions, int userId,
            string reqController, string reqAction, string linkText, int reqActionId = 0)
        {
            //bool isAllowed = checkPermission(userId, reqController, reqAction, reqActionId);
            //if (!isAllowed)
            //{
            //    return MvcHtmlString.Empty;
            //}

            return ajaxHelper.ActionLink(
                linkText,
                reqAction,
                new {id = reqActionId},
                ajaxOptions
                );
        }

        public class MyDateTimeConverter : Newtonsoft.Json.JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof (DateTime);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                JsonSerializer serializer)
            {
                var t = long.Parse((string) reader.Value);
                return new DateTime(1970, 1, 1).AddMilliseconds(t);
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

        }
    }
}