using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TFG_Web.Services
{
    public class ViewLayoutAttribute : ResultFilterAttribute
    {
        private readonly string _layout;
        private const string Layout = "Layout";

        #region "Constructor"
        public ViewLayoutAttribute(string layout)
        {
            this._layout = layout;
        }
        #endregion

        /// <summary>
        /// Para poder utilizar diferentes Layouts depende del parametro
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var viewResult = context.Result as ViewResult;
            if (viewResult != null)
            {
                viewResult.ViewData[Layout] = this._layout;
            }
        }
    }
}