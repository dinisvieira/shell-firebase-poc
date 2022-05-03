using System.ComponentModel;
using ToDoList.ViewModels;
using Xamarin.Forms;

namespace ToDoList.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}