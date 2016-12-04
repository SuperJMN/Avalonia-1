namespace OmniXaml.Avalonia.Context
{
    using System;
    using System.Diagnostics;
    using global::Avalonia.Data;
    using Glass.Core;

    public class AvaloniaObjectBuilder : ExtendedObjectBuilder
    {
        private readonly IContextFactory contextFactory;

        public AvaloniaObjectBuilder(IInstanceCreator instanceCreator, ObjectBuilderContext objectBuilderContext, IContextFactory contextFactory)
            : base(instanceCreator, objectBuilderContext, contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        protected override void PerformAssigment(Assignment assignmentTarget, BuildContext trackingContext)
        {
            var compatibleValue = MakeCompatible(assignmentTarget.Target.Instance, new ConversionRequest(assignmentTarget.Member, assignmentTarget.Value),  trackingContext);

            if (assignmentTarget.Member.MemberType.IsCollection() && !(compatibleValue is IBinding))
            {
                if (compatibleValue.GetType().IsCollection())
                {
                    assignmentTarget.ExecuteAssignment();
                }
                else
                {
                    var collection = assignmentTarget.Member.GetValue(assignmentTarget.Target.Instance);
                    var child = new KeyedInstance(compatibleValue, assignmentTarget.Target.Key);

                    Associate(new ChildAssociation(collection, child), trackingContext);                                       
                }                
            }
            else
            {
                var context = contextFactory.CreateConverterContext(assignmentTarget.Member.MemberType, compatibleValue, trackingContext);
                PropertyAccessor.SetValue(assignmentTarget.Target.Instance, assignmentTarget.Member, assignmentTarget.Value, context);
            }

            OnInstanceAssociated(trackingContext, compatibleValue);
        }
    }
}