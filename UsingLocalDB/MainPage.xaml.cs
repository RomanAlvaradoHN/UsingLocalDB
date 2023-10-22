using Microsoft.Maui.Controls;
using UsingLocalDB.Controllers;
using UsingLocalDB.Models;
using UsingLocalDB.Views;

namespace UsingLocalDB {
    public partial class MainPage : ContentPage {
        private readonly AppDataBase db;

        public MainPage() {
            InitializeComponent();
            db = new AppDataBase();
        }

        private async void OnBtnGuadarDatosClicked(object sender, EventArgs e) {
            Persona persona = new Persona(
                GetFotoFromImageButton(),
                txtNombre.Text,
                txtApellido.Text,
                txtDNI.Text
            ) ;

            if (!persona.GetDatosInvalidos().Any()) {
                await db.Insert(persona);
                await Navigation.PushAsync(new PageResults());
                LimpiarCampos();

            } else {
                string msj = string.Join("\n", persona.GetDatosInvalidos());
                await DisplayAlert("Datos Invalidos:", msj, "acepar");
            }
        }


        public async void TakeAPhoto(object sender, EventArgs e) {
            if (MediaPicker.Default.IsCaptureSupported) {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
                if (photo != null) {
                    try {
                        using Stream sourceStream = await photo.OpenReadAsync();

                        // Convierte la imagen a un arreglo de bytes (byte[])
                        byte[] imageData;
                        using (MemoryStream memoryStream = new MemoryStream()) {
                            await sourceStream.CopyToAsync(memoryStream);
                            imageData = memoryStream.ToArray();
                        }

                        // Convierte el arreglo de bytes en un FileImageSource
                        var imageSource = FileImageSource.FromStream(() => new MemoryStream(imageData));

                        // Asigna la imagen al ImageButton
                        txtFoto.Source = imageSource;
                    } catch (Exception ex) {
                        // Manejo de excepciones
                        await DisplayAlert("Atención", ex.Message, "Aceptar");
                    }
                }
            }
        }


        private byte[] GetFotoFromImageButton() {
            FileImageSource fileImageSource = (FileImageSource)txtFoto.Source;

            if (fileImageSource != null) {
                // Obtén la ruta del archivo de la imagen desde la propiedad File de FileImageSource
                string imagePath = fileImageSource.File;

                if (!string.IsNullOrEmpty(imagePath)) {
                    // Lee la imagen desde el archivo y conviértela en un arreglo de bytes
                    return File.ReadAllBytes(imagePath);
                }
            }
            return new byte[0];
        }





        private void LimpiarCampos() {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtDNI.Text = string.Empty;
        }
    }
}