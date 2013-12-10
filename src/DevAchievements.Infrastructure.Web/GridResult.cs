using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelperSharp;

namespace DevAchievements.Infrastructure.Web
{
    /// <summary>
    /// JsonResult para o component Grid.js.
    /// </summary>
    /// <typeparam name="TModel">O modelo que será renderizado na grid.</typeparam>
    /// <typeparam name="TId">O tipo de id que o model usa.</typeparam>
    public class GridResult<TModel, TId> : JsonResult
    {
        /// <summary>
        /// Inicia uma nova instância da classe <see cref="GridResult{TModel,TId}" />.
        /// </summary>
        /// <param name="models">The models.</param>
        /// <param name="getModelIdFunc">The get model id func.</param>
        /// <param name="getValuesFunc">The get values func.</param>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Validado por ThrowIfNull."),
        SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Justification = "Validado por ThrowIfNull."), 
        SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "2", Justification = "Validado por ThrowIfNull."), 
        SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public GridResult(IEnumerable<TModel> models, Func<TModel, TId> getModelIdFunc, Func<TModel, IEnumerable<object>> getValuesFunc)
        {
            ExceptionHelper.ThrowIfNull("models", models);
            ExceptionHelper.ThrowIfNull("getModelIdFunc", getModelIdFunc);
            ExceptionHelper.ThrowIfNull("getValuesFunc", getValuesFunc);

            JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            var data = new List<string[]>();

            foreach (var m in models)
            {
                var id = getModelIdFunc(m).ToString();
                var values = new List<object>(getValuesFunc(m));
                values.Insert(0, id); // Coluna do id.

                if (HttpContext.Current.Request["checkboxEnabled"] == "true")
                {
                    values.Insert(1, String.Empty);
                }

                values.Add(id);       // Coluna delete.
                data.Add(values.Select(v => v != null ? v.ToString() : String.Empty).ToArray());
            }

            Data = new
            {
                iTotalRecords = data.Count,
                sEcho = HttpContext.Current.Request["sEcho"],
                aaData = data
            };
        }
    }
}