namespace OmniXaml.Avalonia.Context
{
    using System;
    using global::Avalonia.Data;
    using Glass.Core;

    public class AvaloniaObjectBuilder : ExtendedObjectBuilder
    {
        private readonly Func<Assignment, ObjectBuilderContext, BuildContext, ValueContext> contextFactory;

        public AvaloniaObjectBuilder(ObjectBuilderContext constructionContext, Func<Assignment, ObjectBuilderContext, BuildContext, ValueContext> contextFactory)
            : base(constructionContext, contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        protected override void PerformAssigment(Assignment assignmentTarget, BuildContext trackingContext)
        {
            var compatibleValue = ToCompatibleValue(assignmentTarget, trackingContext);

            if (compatibleValue.Property.PropertyType.IsCollection() && !(compatibleValue.Value is IBinding))
            {
                Utils.UniversalAdd(compatibleValue.Property.GetValue(compatibleValue.Instance), compatibleValue.Value);
            }
            else
            {
                var context = this.contextFactory(compatibleValue, ObjectBuilderContext, trackingContext);
                PropertyAccessor.SetValue(compatibleValue.Instance, compatibleValue.Property, compatibleValue.Value, context);
            }
        }
    }
}