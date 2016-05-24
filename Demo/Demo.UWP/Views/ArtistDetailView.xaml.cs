﻿using MvvmCross.WindowsUWP.Views;

// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=234238

namespace Demo.UWP.Views
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    [MvxRegion("FrameContent")]
    public sealed partial class ArtistDetailView : BaseView
    {
        public ArtistDetailView()
        {
            this.InitializeComponent();
        }
    }
}