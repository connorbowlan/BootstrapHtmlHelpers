﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using BootstrapHtmlHelpersPackage.Builders;
using InputType = BootstrapHtmlHelpersPackage.Enums.InputType;

namespace BootstrapHtmlHelpersPackage.Extensions
{
    public static class FormExtension
    {
        #region FormSubmitButton

        public static MvcHtmlString FormSubmitButton(this HtmlHelper htmlHelper, string value, string id = null, object htmlAttributes = null)
        {
            return FormBuilder.CreateFormSubmitButton(htmlHelper, value, id, htmlAttributes);
        }

        #endregion

        #region JsButton

        // TODO: Expand to accept a variety of event types instead of just OnClick.
        public static MvcHtmlString JsButton(this HtmlHelper htmlHelper, string value, string onClick, string id = null, object htmlAttributes = null)
        {
            return FormBuilder.CreateJsButton(htmlHelper, value, onClick, id, htmlAttributes);
        }

        #endregion

        #region FormGroupFor

        public static MvcHtmlString FormGroupFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            InputType inputType = InputType.Default,
            string format = null,
            object htmlAttributes = null)
        {
            // If an input type is passed, use that input type.
            if (inputType != InputType.Default)
            {
                return FormBuilder.CreateFormGroup(htmlHelper, expression, inputType, format, htmlAttributes);
            }

            var dataType = typeof(TProperty).Name;

            switch (dataType)
            {
                case "Char":
                    return FormBuilder.CreateFormGroup(htmlHelper, expression, InputType.Text, format, htmlAttributes);

                case "String":
                    return FormBuilder.CreateFormGroup(htmlHelper, expression, InputType.Text, format, htmlAttributes);

                case "Int32":
                    return FormBuilder.CreateFormGroup(htmlHelper, expression, InputType.Number, format, htmlAttributes);

                case "Double":
                    return FormBuilder.CreateFormGroup(htmlHelper, expression, InputType.Number, format, htmlAttributes);

                case "Int64": // Long
                    return FormBuilder.CreateFormGroup(htmlHelper, expression, InputType.Number, format, htmlAttributes);

                case "Single": // Float
                    return FormBuilder.CreateFormGroup(htmlHelper, expression, InputType.Number, format, htmlAttributes);

                case "Boolean":
                    return FormBuilder.CreateFormGroup(htmlHelper, expression, InputType.Checkbox, format,
                        htmlAttributes);

                case "DateTime":
                    return FormBuilder.CreateFormGroup(htmlHelper, expression, InputType.Date, format, htmlAttributes);
            }

            return MvcHtmlString.Create(
                "The  data type of the property you are passing to @Html.FormGroupFor() is not supported.");
        }

        public static MvcHtmlString FormGroupFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            IEnumerable<SelectListItem> selectList,
            string optionLabel = null,
            object htmlAttributes = null)
        {
            return FormBuilder.CreateFormGroup(htmlHelper, expression, selectList, optionLabel, htmlAttributes);
        }

        #endregion
    }
}