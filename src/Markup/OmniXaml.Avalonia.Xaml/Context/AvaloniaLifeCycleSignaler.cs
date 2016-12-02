namespace OmniXaml.Avalonia.Context
{
    using System.Diagnostics;
    using global::Avalonia;

    public class AvaloniaLifeCycleSignaler : IInstanceLifecycleSignaler
    {
        public void OnBegin(object instance)
        {          
            var isi = instance as ISupportInitialize;            
            isi?.BeginInit();
        }

        public void OnEnd(object instance)
        {
            var isi = instance as ISupportInitialize;

            if (instance.GetType().Name.Equals("MyCustomWindow"))
            {
                Debugger.Break();
            }

            isi?.EndInit();
        }

        public void AfterAssociatedToParent(object instance)
        {

        }
    }
}