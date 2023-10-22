using UsingLocalDB.Controllers;
using UsingLocalDB.Models;

namespace UsingLocalDB.Views;

public partial class PageResults : ContentPage
{
    private readonly AppDataBase db;


	public PageResults(){
		InitializeComponent();
        db = new AppDataBase();
    }



    protected override async void OnAppearing() {
        base.OnAppearing();
        myCollectionView.ItemsSource = await db.SelectAll();
    }
}