namespace Sfa.Das.Sas.Shared.Components.ViewModels.Css.Interfaces
{
    public interface IUtilitiesCssViewModel
    {
        string ClassPrefix { get; set; }
        string FontWeightBold { get; }
        string Margin(string type, int size);


    }
}
