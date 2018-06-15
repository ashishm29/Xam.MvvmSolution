using Acr.UserDialogs;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xam.Mvvm.ViewModel.Model;
using Xamarin.Forms;

namespace Xam.Mvvm.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        #region Properties

        #region DisplayList
        public const string DisplayListPropertyName = "DisplayList";

        private ObservableCollection<Country> _DisplayList;

        public ObservableCollection<Country> DisplayList
        {
            get
            {
                return _DisplayList;
            }

            set
            {
                if (_DisplayList == value)
                {
                    return;
                }

                _DisplayList = value;
                RaisePropertyChanged(DisplayListPropertyName);
            }
        }
        #endregion

        #region SelectedCountry
        public const string SelectedCountryPropertyName = "SelectedCountry";

        private Country _SelectedCountry;

        public Country SelectedCountry
        {
            get
            {
                return _SelectedCountry;
            }

            set
            {
                if (_SelectedCountry == value)
                {
                    return;
                }

                _SelectedCountry = value;
                RaisePropertyChanged(SelectedCountryPropertyName);
            }
        }
        #endregion


        #region CountryName
        public const string CountryNamePropertyName = "CountryName";

        private string _CountryName = "ADD MORE";

        public string CountryName
        {
            get
            {
                return _CountryName;
            }

            set
            {
                if (_CountryName == value)
                {
                    return;
                }

                _CountryName = value;
                RaisePropertyChanged(CountryNamePropertyName);
            }
        }
        #endregion

        #endregion

        public RelayCommand<Country> ItemSelectedCommand { get; set; }

        public RelayCommand<Country> ItemTappedCommand { get; set; }

        public RelayCommand ButtonClickCommand { get; set; }

        #region Ctor
        public MainViewModel()
        {

            ItemSelectedCommand = new RelayCommand<Country>((model) => ItemSelectedCommandReceiver(model));
            ItemTappedCommand = new RelayCommand<Country>((model) => ItemTappedCommandReceiver(model));
            ButtonClickCommand = new RelayCommand(ButtonClickCommandReceiver);

            DisplayList = new ObservableCollection<Country>();
            DisplayList.Add(new Country { Name = "INDIA" });
            DisplayList.Add(new Country { Name = "AMERICA" });
            //DisplayList.Add(new Country { Name = "AUSTRALIA" });
            //DisplayList.Add(new Country { Name = "SINGAPORE" });
            //DisplayList.Add(new Country { Name = "NEPAL" });
            //DisplayList.Add(new Country { Name = "PERU" });

        }

        private void ItemSelectedCommandReceiver(Country model)
        {
            if (SelectedCountry != null)
            {
                CountryName = SelectedCountry.Name;
            }
        }
        private void ItemTappedCommandReceiver(Country model)
        {
            if (SelectedCountry != null)
            {
                CountryName = SelectedCountry.Name;
                int index = DisplayList.IndexOf(SelectedCountry);
                DisplayList.Move(index, 0);
            }
        }

        private async void ButtonClickCommandReceiver()
        {
            try
            {
                bool result = true;
                if (DisplayList == null)
                    DisplayList = new ObservableCollection<Country>();

                UserDialogs.Instance.ShowLoading("Loading...");

                await Task.Delay(1500); // Service Hit.

                result = await Application.Current.MainPage.DisplayAlert("Alert", "Add Item ?", "Yes", "No");

                DisplayList.Add(new Country { Name = "NEW COUNTRY : " + DisplayList.Count.ToString() });

                UserDialogs.Instance.HideLoading();
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }



        #endregion
    }
}