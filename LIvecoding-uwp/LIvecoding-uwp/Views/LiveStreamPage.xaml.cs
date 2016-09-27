using LIvecoding_uwp.Configuration;
using LivecodingApi.Model;
using LivecodingApi.Services;
using System;
using Windows.UI.Xaml.Controls;

// Pour plus d'informations sur le modèle d'élément Page vierge, voir la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace LIvecoding_uwp.Views
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class LiveStreamPage : Page
    {
        public LiveStreamPage()
        {
            this.InitializeComponent();
            ((ViewModels.LiveStreamViewModel)DataContext).ChargerStream();
        }
    }
}
