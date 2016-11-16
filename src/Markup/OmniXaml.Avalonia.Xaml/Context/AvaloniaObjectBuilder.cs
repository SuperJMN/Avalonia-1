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

        protected override void PerformAssigment(Assignment assignmentTarget, BuildContext trackingContext, string key)
        {
            var compatibleValue = ToCompatibleValue(assignmentTarget, trackingContext);

            if (compatibleValue.Member.MemberType.IsCollection() && !(compatibleValue.Value is IBinding))
            {
                if (compatibleValue.Value.GetType().IsCollection())
                {
                    assignmentTarget.ExecuteAssignment();
                }
                else
                {
                    Utils.UniversalAdd(compatibleValue.Member.GetValue(compatibleValue.Instance), compatibleValue.Value);
                }                
            }
            else
            {
                var context = contextFactory.CreateConverterContext(assignmentTarget.Member.MemberType, compatibleValue, trackingContext);
                PropertyAccessor.SetValue(compatibleValue.Instance, compatibleValue.Member, compatibleValue.Value, context);
            }            
        }
    }
}