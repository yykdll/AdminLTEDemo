using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;

namespace System.Web.Mvc
{
    public static class InputHelper
    {

        public static MvcHtmlString CheckBoxList<TModel, T>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, T>> expression, IEnumerable<SelectListItem> source, bool AppendLi = false, object htmlAttributes = null, bool ReadOnly = false)
        {
            return htmlHelper.smethod_0<TModel, T>(expression, source, InputType.CheckBox, AppendLi, htmlAttributes, ReadOnly);
        }

        public static MvcHtmlString RadioList<TModel, T>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, T>> expression, IEnumerable<SelectListItem> source, bool AppendLi = false, object htmlAttributes = null, bool ReadOnly = false)
        {
            return htmlHelper.smethod_0<TModel, T>(expression, source, InputType.Radio, AppendLi, htmlAttributes, ReadOnly);
        }

        private static MvcHtmlString smethod_0<TModel, T>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, T>> expression, IEnumerable<SelectListItem> source, InputType inputType_0, bool bool_0, object object_0, bool bool_1 = false)
        {
            object obj = (object)"";
            if ((object)htmlHelper.ViewData.Model != null)
                obj = (object)expression.Compile()(htmlHelper.ViewData.Model);
            string expressionText = ExpressionHelper.GetExpressionText((LambdaExpression)expression);
            StringBuilder stringBuilder = new StringBuilder();
            List<string> stringList = new List<string>();
            if (obj != null)
            {
                if (obj.ToString().Contains(","))
                {
                    string str1 = obj.ToString();
                    char[] chArray = new char[1] { ',' };
                    foreach (string str2 in str1.Split(chArray))
                    {
                        if (!string.IsNullOrEmpty(str2))
                            stringList.Add(str2);
                    }
                }
                else
                    stringList.Add(obj.ToString());
            }
            foreach (SelectListItem selectListItem in source)
            {
                if (bool_1)
                {
                    if (stringList.Contains(selectListItem.Value))
                        stringBuilder.Append(selectListItem.Text + " ");
                }
                else
                {
                    TagBuilder tagBuilder = new TagBuilder("input");
                    tagBuilder.Attributes["type"] = inputType_0.ToString();
                    tagBuilder.MergeAttribute("name", expressionText);
                    tagBuilder.MergeAttribute("id", expressionText);
                    tagBuilder.MergeAttribute("value", selectListItem.Value);
                    if (stringList.Contains(selectListItem.Value))
                        tagBuilder.Attributes["checked"] = "true";
                    tagBuilder.MergeAttributes<string, object>((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(object_0));
                    stringBuilder.Append((bool_0 ? "<li>" : "") + tagBuilder.ToString(TagRenderMode.SelfClosing) + selectListItem.Text + "&nbsp;" + (bool_0 ? "</li>" : ""));
                }
            }
            return MvcHtmlString.Create((bool_0 ? "<ul>" : "") + stringBuilder.ToString() + (bool_0 ? "</ul>" : ""));
        }

        public static MvcHtmlString DropDownList<TModel, T>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, T>> expression, IEnumerable<SelectListItem> source, SelectListItem defaultValue = null, string title = "", object htmlAttributes = null, bool ReadOnly = false)
        {
            object obj = (object)"";
            if ((object)htmlHelper.ViewData.Model != null)
                obj = (object)expression.Compile()(htmlHelper.ViewData.Model);
            string expressionText = ExpressionHelper.GetExpressionText((LambdaExpression)expression);
            TagBuilder tagBuilder1 = new TagBuilder("select");
            tagBuilder1.MergeAttribute("name", expressionText);
            tagBuilder1.MergeAttribute("id", expressionText);
            if (!string.IsNullOrEmpty(title))
                tagBuilder1.MergeAttribute("title", title);
            if (defaultValue != null)
            {
                TagBuilder tagBuilder2 = new TagBuilder("option")
                {
                    InnerHtml = defaultValue.Text
                };
                tagBuilder2.Attributes["value"] = defaultValue.Value;
                tagBuilder1.InnerHtml += tagBuilder2.ToString();
            }
            foreach (SelectListItem selectListItem in source)
            {
                if (ReadOnly)
                {
                    if ((obj == null ? 0 : (obj.ToString().Equals(selectListItem.Value, StringComparison.CurrentCultureIgnoreCase) ? 1 : 0)) != 0)
                        return MvcHtmlString.Create(selectListItem.Text);
                }
                else
                {
                    TagBuilder tagBuilder2 = new TagBuilder("option");
                    List<string> stringList = new List<string>();
                    bool flag = obj != null && obj.ToString().Equals(selectListItem.Value, StringComparison.CurrentCultureIgnoreCase);
                    tagBuilder2.InnerHtml = selectListItem.Text;
                    tagBuilder2.Attributes["value"] = selectListItem.Value;
                    if (flag)
                        tagBuilder2.Attributes["selected"] = "selected";
                    tagBuilder1.InnerHtml += tagBuilder2.ToString();
                }
            }
            tagBuilder1.MergeAttributes<string, object>((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            return MvcHtmlString.Create(tagBuilder1.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> source, string value, SelectListItem defaultValue = null, object htmlAttributes = null, bool ReadOnly = false)
        {
            TagBuilder tagBuilder1 = new TagBuilder("select");
            tagBuilder1.MergeAttribute("name", name);
            tagBuilder1.MergeAttribute("id", name);
            if (defaultValue != null)
            {
                TagBuilder tagBuilder2 = new TagBuilder("option")
                {
                    InnerHtml = defaultValue.Text
                };
                tagBuilder2.Attributes["value"] = defaultValue.Value;
                tagBuilder1.InnerHtml += tagBuilder2.ToString();
            }
            foreach (SelectListItem selectListItem in source)
            {
                if (ReadOnly)
                {
                    if ((value == null ? 0 : (value.ToString().Equals(selectListItem.Value, StringComparison.CurrentCultureIgnoreCase) ? 1 : 0)) != 0)
                        return MvcHtmlString.Create(selectListItem.Text);
                }
                else
                {
                    TagBuilder tagBuilder2 = new TagBuilder("option");
                    bool flag = value != null && value.ToString().Equals(selectListItem.Value, StringComparison.CurrentCultureIgnoreCase);
                    tagBuilder2.InnerHtml = selectListItem.Text;
                    tagBuilder2.Attributes["value"] = selectListItem.Value;
                    if (flag)
                        tagBuilder2.Attributes["selected"] = "selected";
                    tagBuilder1.InnerHtml += tagBuilder2.ToString();
                }
            }
            tagBuilder1.MergeAttributes<string, object>((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            return MvcHtmlString.Create(tagBuilder1.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString SelectList<TModel, T>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, T>> expression, IEnumerable<SelectListItem> source, SelectListItem defaultValue = null, string title = "", object htmlAttributes = null)
        {
            object obj = (object)"";
            if ((object)htmlHelper.ViewData.Model != null)
                obj = (object)expression.Compile()(htmlHelper.ViewData.Model);
            string expressionText = ExpressionHelper.GetExpressionText((LambdaExpression)expression);
            TagBuilder tagBuilder1 = new TagBuilder("select");
            tagBuilder1.MergeAttribute("multiple", "multiple");
            tagBuilder1.MergeAttribute("name", expressionText);
            tagBuilder1.MergeAttribute("id", expressionText);
            if (!string.IsNullOrEmpty(title))
                tagBuilder1.MergeAttribute("title", title);
            if (defaultValue != null)
            {
                TagBuilder tagBuilder2 = new TagBuilder("option")
                {
                    InnerHtml = defaultValue.Text
                };
                tagBuilder2.Attributes["selected"] = "selected";
                tagBuilder2.Attributes["value"] = defaultValue.Value;
                tagBuilder1.InnerHtml += tagBuilder2.ToString();
            }
            foreach (SelectListItem selectListItem in source)
            {
                TagBuilder tagBuilder2 = new TagBuilder("option");
                List<string> stringList = new List<string>();
                if (obj != null)
                {
                    if (obj.ToString().Contains(","))
                    {
                        string str1 = obj.ToString();
                        char[] chArray = new char[1] { ',' };
                        foreach (string str2 in str1.Split(chArray))
                        {
                            if (!string.IsNullOrEmpty(str2))
                                stringList.Add(str2);
                        }
                    }
                    else
                        stringList.Add(obj.ToString());
                }
                if (obj != null)
                    obj.ToString().Equals(selectListItem.Value, StringComparison.CurrentCultureIgnoreCase);
                tagBuilder2.InnerHtml = selectListItem.Text;
                tagBuilder2.Attributes["value"] = selectListItem.Value;
                if (stringList.Contains(selectListItem.Value))
                    tagBuilder2.Attributes["selected"] = "selected";
                tagBuilder1.InnerHtml += tagBuilder2.ToString();
            }
            tagBuilder1.MergeAttributes<string, object>((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            return MvcHtmlString.Create(tagBuilder1.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString CheckBoxListExt(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> source, string value, SelectListItem defaultValue = null, object htmlAttributes = null)
        {
            string str = "";
            foreach (SelectListItem selectListItem in source)
            {
                TagBuilder tagBuilder = new TagBuilder("input");
                tagBuilder.Attributes["type"] = InputType.CheckBox.ToString();
                tagBuilder.MergeAttribute("name", name);
                tagBuilder.MergeAttribute("id", name);
                if ((value == null ? 0 : (value.ToString().Equals(selectListItem.Value, StringComparison.CurrentCultureIgnoreCase) ? 1 : 0)) != 0)
                    tagBuilder.Attributes["checked"] = "checked";
                tagBuilder.Attributes["value"] = selectListItem.Value;
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                str = str + tagBuilder.ToString(TagRenderMode.SelfClosing) + selectListItem.Text;
            }
            return MvcHtmlString.Create(str);
        }

        public static MvcHtmlString Hidden<TModel, T>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, T>> expression)
        {
            return htmlHelper.smethod_1<TModel, T>(expression, InputType.Hidden, "", (object)null, false);
        }

        public static MvcHtmlString Hidden<TModel, T>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, T>> expression, object htmlAttributes)
        {
            return htmlHelper.smethod_1<TModel, T>(expression, InputType.Hidden, "", htmlAttributes, false);
        }

        public static MvcHtmlString Password<TModel, T>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, T>> expression, string title = "", object htmlAttributes = null, bool ReadOnly = false)
        {
            return htmlHelper.smethod_1<TModel, T>(expression, InputType.Password, title, htmlAttributes, ReadOnly);
        }

        public static MvcHtmlString TextBox<TModel, T>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, T>> expression, string title = "", object htmlAttributes = null, bool ReadOnly = false)
        {
            return htmlHelper.smethod_1<TModel, T>(expression, InputType.Text, title, htmlAttributes, ReadOnly);
        }

        public static MvcHtmlString CheckBox<TModel, T>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, T>> expression, string title = "", object htmlAttributes = null, bool ReadOnly = false)
        {
            return htmlHelper.smethod_1<TModel, T>(expression, InputType.CheckBox, title, htmlAttributes, ReadOnly);
        }

        public static MvcHtmlString RadioInput<TModel, T>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, T>> expression, string title = "", object htmlAttributes = null, bool ReadOnly = false)
        {
            return htmlHelper.smethod_1<TModel, T>(expression, InputType.Radio, title, htmlAttributes, ReadOnly);
        }

        private static MvcHtmlString smethod_1<TModel, T>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, T>> expression, InputType inputType_0, string string_0, object object_0, bool bool_0 = false)
        {
            object str1 = (object)"";
            if ((object)htmlHelper.ViewData.Model != null)
                str1 = (object)expression.Compile()(htmlHelper.ViewData.Model);
            if (bool_0)
                return MvcHtmlString.Create((str1 ?? (object)"").ToString());
            string expressionText = ExpressionHelper.GetExpressionText((LambdaExpression)expression);
            ModelMetadata.FromLambdaExpression<TModel, T>(expression, htmlHelper.ViewData);
            TagBuilder tagBuilder = new TagBuilder("input");
            tagBuilder.Attributes["type"] = inputType_0.ToString().ToLower();
            tagBuilder.MergeAttribute("name", expressionText);
            tagBuilder.MergeAttribute("id", expressionText);
            if (!string.IsNullOrEmpty(string_0))
                tagBuilder.MergeAttribute("title", string_0);
            tagBuilder.ToString(TagRenderMode.SelfClosing);
            if (inputType_0 != InputType.CheckBox && inputType_0 != InputType.Radio)
            {
                if (str1 != null)
                    tagBuilder.Attributes["value"] = str1.ToString();
            }
            else
            {
                if (str1.ToBool())
                    tagBuilder.Attributes["checked"] = "checked";
                tagBuilder.Attributes["value"] = "true";
            }
            tagBuilder.MergeAttributes<string, object>((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(object_0));
            string str2 = tagBuilder.ToString(TagRenderMode.SelfClosing);
            if (inputType_0 == InputType.CheckBox || inputType_0 == InputType.Radio)
                str2 = str2 + "<input type=\"hidden\" name=\"" + expressionText + "\" value=\"false\"/>";
            return MvcHtmlString.Create(str2);
        }

        public static MvcHtmlString TextArea<TModel, T>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, T>> expression, object htmlAttributes = null, bool ReadOnly = false)
        {
            object obj = (object)"";
            if ((object)htmlHelper.ViewData.Model != null)
                obj = (object)expression.Compile()(htmlHelper.ViewData.Model);
            if (ReadOnly)
                return MvcHtmlString.Create((obj ?? (object)"").ToString());
            string expressionText = ExpressionHelper.GetExpressionText((LambdaExpression)expression);
            ModelMetadata.FromLambdaExpression<TModel, T>(expression, htmlHelper.ViewData);
            TagBuilder tagBuilder = new TagBuilder("textarea");
            if (obj != null)
                tagBuilder.InnerHtml = obj.ToString();
            tagBuilder.MergeAttribute("name", expressionText);
            tagBuilder.MergeAttribute("id", expressionText);
            tagBuilder.MergeAttributes<string, object>((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        }
    }
}

