using UsingLocalDB.Controllers;
using UsingLocalDB.Models;
using UsingLocalDB.Views;

namespace UsingLocalDB {
    public partial class MainPage : ContentPage {
        AppDataBase db;

        public MainPage() {
            InitializeComponent();
            db = new AppDataBase();
        }

        private async void OnBtnGuadarDatosClicked(object sender, EventArgs e) {
            Persona persona = new Persona(
                txtNombre.Text,
                txtApellido.Text,
                txtDNI.Text
            );

            if (!persona.GetDatosInvalidos().Any()) {
                await db.CreateOrUpdate(persona);
                await Navigation.PushAsync(new PageResults());
                LimpiarCampos();

            } else {
                string msj = string.Join("\n", persona.GetDatosInvalidos());
                await DisplayAlert("Datos Invalidos:", msj, "acepar");
            }
        }



        private void LimpiarCampos() {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtDNI.Text = string.Empty;
        }
    }
}