namespace OmniXaml.Avalonia.Context
{
    using global::Avalonia;

    public class AvaloniaLifeCycleSignaler : IInstanceLifecycleSignaler
    {
        public void BeforeAssigments(object instance)
        {
            var isi = instance as ISupportInitialize;
            isi?.BeginInit();
        }

        public void AfterAssigments(object instance)
        {
            var isi = instance as ISupportInitialize;
            isi?.EndInit();
        }
    }
}