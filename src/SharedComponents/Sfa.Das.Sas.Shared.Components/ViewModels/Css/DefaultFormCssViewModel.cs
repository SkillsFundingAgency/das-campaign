namespace Sfa.Das.Sas.Shared.Components.ViewModels.Css
{
    public class DefaultFormCssViewModel : IDefaultFormCssViewModel
    {
        public DefaultFormCssViewModel(string classPrefix)
        {
            _classPrefix = classPrefix;
        }
        private readonly string _classPrefix;
        public string Input => $"{_classPrefix}input";
        public string InputWidth2 => $"{_classPrefix}input--width-2";
        public string InputWidth3 => $"{_classPrefix}input--width-3";
        public string InputWidth4 => $"{_classPrefix}input--width-4";
        public string InputWidth5 => $"{_classPrefix}input--width-5";
        public string InputWidth10 => $"{_classPrefix}input--width-10";
        public string InputWidth20 => $"{_classPrefix}input--width-20";
        public string InputWidth30 => $"{_classPrefix}input--width-30";

        public string FormGroup => $"{_classPrefix}form-group";

        public string Fieldset => $"{_classPrefix}fieldset";
        public string Legend => $"{_classPrefix}fieldset__legend";
        public string LegendLarge => $"{_classPrefix}fieldset__legend--l";
        public string LegendXLarge => $"{_classPrefix}fieldset__legend--xl";

        public string Radio => $"{_classPrefix}radios";
        public string RadioInput => $"{_classPrefix}radios__input";
        public string RadioLabel => $"{_classPrefix}radios__label";
        public string RadioItem => $"{_classPrefix}radios__item";
        public string RadioGroupInline => $"{Radio} radios--inline";
        public string Label => $"{_classPrefix}label";
        public string LabelLarge => $"{_classPrefix}label--l";
        public string LabelXLarge => $"{_classPrefix}label--xl";

        public string Checkbox => $"{_classPrefix}checkbox";
        public string CheckboxInput => $"{_classPrefix}checkboxes__input";
        public string CheckboxItem => $"{_classPrefix}checkboxes__item";
        public string CheckboxLabel => $"{Label} {_classPrefix}checkboxes__label";
    }
}
