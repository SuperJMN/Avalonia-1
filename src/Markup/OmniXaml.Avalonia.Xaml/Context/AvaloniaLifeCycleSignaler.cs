namespace OmniXaml.Avalonia.Context
{
    using global::Avalonia;

    public class AvaloniaLifeCycleSignaler : IInstanceLifecycleSignaler
    {
        public void OnBegin(object instance)
        {          
            var isi = instance as ISupportInitialize;            
            isi?.BeginInit();
        }

        public void EndEnd(object instance)
        {
            var isi = instance as ISupportInitialize;
            isi?.EndInit();
        }

        public void AfterAssociatedToParent(object instance)
        {

        }
    }
}