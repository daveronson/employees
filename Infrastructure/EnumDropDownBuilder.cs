namespace EmployeeDirectory.Infrastructure
{
    using System;
    using System.Reflection;
    using HtmlTags;
    using HtmlTags.Conventions;
    using HtmlTags.Conventions.Elements;

    public class EnumDropDownBuilder : ElementTagBuilder
    {
        public override bool Matches(ElementRequest subject)
        {
            return subject.Accessor.PropertyType.GetTypeInfo().IsEnum;
        }

        public override HtmlTag Build(ElementRequest request)
        {
            var enumType = request.Accessor.PropertyType;

            var select = new SelectTag();

            foreach (var value in Enum.GetValues(enumType))
            {
                select.Option(Enum.GetName(enumType, value), value);
            }
            select.SelectByValue(request.RawValue);

            return select;
        }
    }

}