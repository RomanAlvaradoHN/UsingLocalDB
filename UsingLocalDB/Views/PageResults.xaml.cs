using UsingLocalDB.Controllers;
using UsingLocalDB.Models;

namespace UsingLocalDB.Views;

public partial class PageResults : ContentPage
{

	public PageResults()
	{
		InitializeComponent();
		SetSource();
    }


	private async void SetSource() {
		List<Persona> listaPersonas = await new AppDataBase().SelectAll();
		myListView.ItemsSource = listaPersonas;
    }
}