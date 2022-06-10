using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cine.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PeliculasView : ContentPage
    {
        public PeliculasView()
        {
            InitializeComponent();
        }
        private async void SwipeItem_Clicked(object sender, EventArgs e)
        {
            //Pregunta si desea eliminar
            if (await DisplayAlert("Por favor confirme", "¿Está seguro que desea eliminar la película?", "Si", "No") == true)
            {
                var sw = (SwipeItem)sender;
                a.EliminarCommand.Execute(sw.CommandParameter);
            }
        }
    }
}