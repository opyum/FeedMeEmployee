using FeedMeEmployee.HttpCall;
using FeedMeEmployee.Model;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using TodoREST.Services;

namespace FeedMeEmployee.Views;

public partial class Historique : ContentPage
{
    public ObservableCollection<Facture> Factures { get; set; } = new ObservableCollection<Facture>
    {
        new Facture { DateHeure = DateTime.Now, NomCommerce = "Commerce A", Montant = 25.00m },
        new Facture { DateHeure = DateTime.Now.AddDays(-1), NomCommerce = "Commerce B", Montant = 18.50m },
        // Ajoutez d'autres factures ici
    };

    public IRestService service { get; set; }
    public ApiService ApiService { get; set; }
    public Historique(IRestService service)
    {
        InitializeComponent();
        this.service = service;
        BindingContext = Factures;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        var histoFacture = await this.service.GetHistoFacture();
        var transacList = await this.service.GetAllTransactions();
        List<Facture> factures = histoFacture;
        foreach (var facture in transacList)
        {
            factures.Add(new Facture()
            {
                DateHeure = facture.DateHeure,
                Montant = facture.Montant,
                NomCommerce = "Mon resto test",
            });
        }
        FacturesCollectionView.ItemsSource= factures.OrderByDescending(x => x.DateHeure);

    }

}