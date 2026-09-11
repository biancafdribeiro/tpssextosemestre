namespace MauiApp3;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void BtnOk_Clicked(object sender, EventArgs e)
    {
        if (txtId.Text == "admin" && txtSenha.Text == "senha@dmin")
        {
            await DisplayAlert(
                "Login",
                "Login realizado com sucesso!",
                "OK");
        }
        else
        {
            await DisplayAlert(
                "Login",
                "Login não autorizado.",
                "OK");
        }
    }

    private void BtnLimpar_Clicked(object sender, EventArgs e)
    {
        txtId.Text = "";
        txtSenha.Text = "";

        txtId.Focus();
    }

    private async void BtnCreditos_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert(
            "Créditos",
            "Autora do aplicativo: Bianca Fonseca Dantas Ribeiro - CB3025683",
            "OK");
    }
}