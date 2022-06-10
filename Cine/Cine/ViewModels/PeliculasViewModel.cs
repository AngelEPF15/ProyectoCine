using Cine.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Input;
using Cine.Views;
using Xamarin.Forms;

namespace Cine.ViewModels
{
    public class PeliculasViewModel
    {
        public ObservableCollection<Peliculas> Grupo { get; set; } = new ObservableCollection<Peliculas>();

        public Peliculas peliculas { get; set; }

        public string Error { get; set; }
        public ICommand CambiarVistaCommand { get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand MostrarDetallesCommand { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        AgregarPeliculaView vistaPeliculas;
        DetallesPeliculaView vistaDetalles;
        EditarPeliculaView vistaEditar;


        public PeliculasViewModel()
        {
            Deserializar();
            CambiarVistaCommand = new Command<string>(CambiarVista);
            AgregarCommand = new Command(Agregar);
            MostrarDetallesCommand = new Command<Peliculas>(MostrarDetalles);
            EliminarCommand = new Command<Peliculas>(Eliminar);
            EditarCommand = new Command<Peliculas>(Editar);
            GuardarCommand = new Command(Guardar);
        }

        private void Guardar(object obj)
        {
            Grupo[i] = peliculas;
            Serializar();
            App.Current.MainPage.Navigation.PopToRootAsync();
        }
        private void CambiarVista(string vista)
        {
            if (vista == "Agregar")
            {
                peliculas = new Peliculas();
                vistaPeliculas = new AgregarPeliculaView() { BindingContext = this };
                Application.Current.MainPage.Navigation.PushAsync(vistaPeliculas);
            }
            else if (vista == "Ver")
            {
                Application.Current.MainPage.Navigation.PopToRootAsync();
            }
        }

        int i;
        private async void Editar(Peliculas p)
        {

            i = Grupo.IndexOf(p);
            peliculas = new Peliculas
            {
                Titulo = p.Titulo,
                Año = p.Año,
                Director = p.Director,
                Genero = p.Genero,
                Productora = p.Productora
            };
            vistaEditar = new EditarPeliculaView()
            {
                BindingContext = this
            };
            await App.Current.MainPage.Navigation.PushAsync(vistaEditar);
        }

        private void MostrarDetalles(Peliculas m)
        {
            vistaDetalles = new DetallesPeliculaView()
            {
                BindingContext = m
            };
            App.Current.MainPage.Navigation.PushAsync(vistaDetalles);
        }

        private void Eliminar(Peliculas a)
        {
            if (a != null)
            {
                Grupo.Remove(a);
                Serializar();
            }
        }


        void Serializar()
        {
            var file = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "grupo.json";
            File.WriteAllText(file, JsonConvert.SerializeObject(Grupo));
        }
        void Deserializar()
        {
            var file = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "grupo.json";
            if (File.Exists(file))
            {
                Grupo = JsonConvert.DeserializeObject<ObservableCollection<Peliculas>>(File.ReadAllText(file));
            }
        }
        private void Agregar()
        {
            Error = "";

            if (peliculas != null)
            {
                if (string.IsNullOrEmpty(peliculas.Titulo))
                {
                    Error = "Escriba el Título de la película";
                }
                if (string.IsNullOrWhiteSpace(peliculas.Genero))
                {
                    Error = "Escriba el genero de la película";
                }
                if (string.IsNullOrWhiteSpace(peliculas.Director))
                {
                    Error = "Escriba el nombre del director de la película";
                }
                if (string.IsNullOrWhiteSpace(peliculas.Productora))
                {
                    Error = "Escriba el nombre de la productora de la película";
                }
                if (string.IsNullOrWhiteSpace(Error))
                {
                    Grupo.Add(peliculas);
                    CambiarVista("Ver");
                    Serializar();
                }
                Change();
            }
        }

        private void Change()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

    }


}

