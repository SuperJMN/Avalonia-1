namespace OmniXaml.Avalonia.Context
{
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
                    Collection.UniversalAdd(assignmentTarget.Member.GetValue(assignmentTarget.Target.Instance), compatibleValue);                    
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