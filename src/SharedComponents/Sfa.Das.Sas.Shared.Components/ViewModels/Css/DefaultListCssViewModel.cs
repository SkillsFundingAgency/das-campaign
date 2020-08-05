using System;
using Sfa.Das.Sas.Shared.Components.ViewModels.Css.Interfaces;

namespace Sfa.Das.Sas.Shared.Components.ViewModels.Css
{
    public class DefaultListCssViewModel : IListCssViewModel
    {
        public DefaultListCssViewModel()
        {

        }
        public DefaultListCssViewModel(string classPrefix)
        {
            _classPrefix = classPrefix;
        }
        public DefaultListCssViewModel(string classPrefix,string classModifier)
        {
            _classPrefix = classPrefix;
            _classModifier = classModifier;
        }
        private readonly string _classModifier = string.Empty;
        private readonly string _classPrefix = "govuk-";

        public string List
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_classModifier))
                {
                    return $"{_classPrefix}list";
                }
                else
                {
                    return $"{_classPrefix}list {_classPrefix}list--{_classModifier}";
                }
            }
        }

        public string ListBullet => $"{_classPrefix}list--bullet";

        public string ListNumber => $"{_classPrefix}list--number";
        public string SearchList
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_classModifier))
                {
                    return $"{List} {ListNumber} das-search-results__list";
                }
                else
                {
                    return $"{List} {ListNumber} das-search-results__list das-search-results__list--{_classModifier}";
                }
            }
        }
    }
}
