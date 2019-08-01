using System.Windows.Threading;

namespace WpfApp1_demo.Interface
{
    public interface IAppDispatcher
    {
        Dispatcher AppDispatcher { get; }
    }
}
