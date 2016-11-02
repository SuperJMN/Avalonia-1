namespace OmniXaml.Avalonia.Context
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Data;
    using global::Avalonia;
    using global::Avalonia.Controls;
    using global::Avalonia.Data;
    using Glass.Core;

    public class AvaloniaObjectBuilder : ExtendedObjectBuilder
    {
        public AvaloniaObjectBuilder(ObjectBuilderContext constructionContext, Func<Assignment, ObjectBuilderContext, TrackingContext, ValueContext> contextFactory)
            : base(constructionContext, contextFactory)
        {
        }

        protected override void PerformAssigment(Assignment assignmentTarget, TrackingContext trackingContext)
        {
            var transform = ToCompatibleValue(assignmentTarget, trackingContext);

            if (transform.Value is IBinding)
            {
                SetBinding(transform);
            }
            else
            {
                if (assignmentTarget.Property.PropertyType.IsCollection())
                {
                    Utils.UniversalAdd(assignmentTarget.Property.GetValue(assignmentTarget.Instance), assignmentTarget.Value);
                }
                else
                {
                    transform.ExecuteAssignment();
                }
            }
        }

        private void SetBinding(Assignment assignmentTarget)
        {
            var control = (IControl)assignmentTarget.Instance;

            var avaloniaProperty = AvaloniaPropertyRegistry.Instance.FindRegistered(control.GetType(), assignmentTarget.Property.PropertyName);
            SetBinding(assignmentTarget.Instance, assignmentTarget.Property, avaloniaProperty, (IBinding)assignmentTarget.Value);
        }

        private static void SetBinding(
            object instance,
            Property member,
            AvaloniaProperty property,
            IBinding binding)
        {
            if (!(AssignBinding(instance, member, binding) ||
                  ApplyBinding(instance, property, binding)))
                throw new InvalidOperationException(
                    $"Cannot assign to '{member.PropertyName}' on '{instance.GetType()}");
        }

        private static bool AssignBinding(object instance, Property member, IBinding binding)
        {
            var property = instance.GetType()
                .GetRuntimeProperties()
                .FirstOrDefault(x => x.Name == member.PropertyName);

            if (property?.GetCustomAttribute<AssignBindingAttribute>() != null)
            {
                property.SetValue(instance, binding);
                return true;
            }

            return false;
        }

        private static bool ApplyBinding(
            object instance,
            AvaloniaProperty property,
            IBinding binding)
        {
            if (property == null)
                return false;

            var control = instance as IControl;

            if (control != null)
            {
                if (property != Control.DataContextProperty)
                    DelayedBinding.Add(control, property, binding);
                else
                    control.Bind(property, binding);
            }
            //else
            //{
            //    // The target is not a control, so we need to find an anchor that will let us look
            //    // up named controls and style resources. First look for the closest IControl in
            //    // the TopDownValueContext.
            //    object anchor = context.TopDownValueContext.StoredInstances
            //        .Select(x => x.Instance)
            //        .OfType<IControl>()
            //        .LastOrDefault();

            //    // If a control was not found, then try to find the highest-level style as the XAML
            //    // file could be a XAML file containing only styles.
            //    if (anchor == null)
            //        anchor = context.TopDownValueContext.StoredInstances
            //            .Select(x => x.Instance)
            //            .OfType<IStyle>()
            //            .FirstOrDefault();

            //    ((IAvaloniaObject)instance).Bind(property, binding, anchor);
            //}

            return true;
        }
    }
}