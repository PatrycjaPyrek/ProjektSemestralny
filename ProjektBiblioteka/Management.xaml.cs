﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjektBiblioteka
{
   


    /// <summary>
    /// Interaction logic for Management.xaml
    /// </summary>
    public partial class Management : Window
    {
        libraryEntitiesDataSet context = new libraryEntitiesDataSet(); //dane z bazy
        
        
        bool dateChanged = false;


        public Management()
        {
            InitializeComponent();
            DataContext = new ComboBoxViewModel();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Borrow_Click(object sender, RoutedEventArgs e)
        {

        }

        private void zwrot(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Wybor opcji z menu -> po nacisnieciu wyswietlaja sie rozne okna formularzy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            int index = OptionsComboBox.SelectedIndex;
            switch (index)
            {
                case 0:
                    AddUser addUser = new AddUser();
                    addUser.Show();

                    break;
                case 1:
                    DodawanieUzytkownikowFormularz();
                    break;
                case 2:
                    EdytowanieUzytkownikaFormularz();

                    break;
                case 3:
                    UsuwanieKlientowFormularz();
                    break;

            }
            
           
        }



        private void EdytowanieUzytkownikaFormularz()
        {
            panel1.Children.Clear();


            List<Klienci> klienci = new List<Klienci>();

            foreach (var item in context.Klienci)
            {
                klienci.Add(item);
            }
            ComboBox listaCombo = new ComboBox();
            listaCombo.ItemsSource = klienci;
            panel1.Children.Add(listaCombo);

        

            //dowod
            Thickness m = new Thickness();
            m.Top = -30;
            m.Left = -70;
            Label dowodLabel = new Label();
            dowodLabel.Content = "Input id";
            dowodLabel.FontSize = 15;
            panel1.Children.Add(dowodLabel);
            TextBox dowod = new TextBox();
            dowod.Margin = m;
            dowod.Width = 200;
            dowod.Height = 25;
            panel1.Children.Add(dowod);

            //imie
            Label imieLabel = new Label();
            imieLabel.Content = "Input firstname";
            imieLabel.FontSize = 15;
            panel1.Children.Add(imieLabel);
            TextBox imie = new TextBox();
            imie.Width = 200;
            imie.Height = 25;
            imie.Margin = m;
            panel1.Children.Add(imie);

            //nazwisko
            Label nazwiskoLabel = new Label();
            nazwiskoLabel.Content = "Input surname";
            nazwiskoLabel.FontSize = 15;
            panel1.Children.Add(nazwiskoLabel);
            TextBox nazwisko = new TextBox();
            nazwisko.Width = 200;
            nazwisko.Height = 25;
            nazwisko.Margin = m;
            panel1.Children.Add(nazwisko);

            //checkbox
            Label checkboxLabel = new Label();
            checkboxLabel.Content = "Choose a gender";
            checkboxLabel.FontSize = 15;
            panel1.Children.Add(checkboxLabel);
            Thickness n = dowod.Margin;
            n.Top = -20;
            n.Left = 30;
            Thickness n2 = dowod.Margin;
            n2.Left = 200;
            n2.Top = -20;
            CheckBox checkW = new CheckBox();
            checkW.Width = 300;
            checkW.Margin = n;
            checkW.Height = 20;
            checkW.Name = "w";
            checkW.Content = "Women";

            CheckBox checkM = new CheckBox();
            checkM.Width = 300;
            checkM.Margin = n2;
            checkM.Height = 20;
            checkM.Name = "w";
            checkM.Content = "Men";

            panel1.Children.Add(checkW);
            panel1.Children.Add(checkM);


            //dataUr
            Thickness t = new Thickness();
            t.Left = -170;
            t.Top = -20;
            DateTime defaultDate = new DateTime(1999, 01, 01);
            Label dataLabel = new Label();
            dataLabel.Content = "Select birth date";
            dataLabel.FontSize = 15;
            panel1.Children.Add(dataLabel);
            DatePicker dataUr = new DatePicker();
            dataUr.SelectedDate = defaultDate;
            dataUr.Text = "Choose";

            dataUr.Width = 100;
            dataUr.Height = 25;
            dataUr.Margin = t;
            panel1.Children.Add(dataUr);

            //ulica
            Label ulicaLabel = new Label();
            ulicaLabel.Content = "Input adress";
            ulicaLabel.FontSize = 15;
            panel1.Children.Add(ulicaLabel);
            TextBox ulica = new TextBox();
            ulica.Width = 200;
            ulica.Height = 25;
            ulica.Margin = m;
            panel1.Children.Add(ulica);
            //kod pocztowy
            Label kodLabel = new Label();
            kodLabel.Content = "Postal code";
            kodLabel.FontSize = 15;
            panel1.Children.Add(kodLabel);
            Thickness kodT = new Thickness();
            kodT.Left = -210;
            kodT.Top = -30;
            TextBox kod = new TextBox();
            kod.Margin = kodT;
            kod.Width = 60;
            kod.Height = 25;
            panel1.Children.Add(kod);
            //miejscowosc
            Label miejscowoscL = new Label();
            miejscowoscL.Content = "Input city";
            miejscowoscL.FontSize = 15;
            panel1.Children.Add(miejscowoscL);
            TextBox miejscowosc = new TextBox();
            miejscowosc.Width = 200;
            miejscowosc.Height = 25;
            miejscowosc.Margin = m;
            panel1.Children.Add(miejscowosc);
            //submit
            Button submit2 = new Button();

            submit2.Width = 80;
            submit2.Height = 20;
            submit2.Content = "Submit";
            panel1.Children.Add(submit2);





            submit2.Click += Submit;

            //  submit2.Click += Submit;
            // NEW ==============================================
            void Submit(object f, RoutedEventArgs g)
            {
                DateTime data = dataUr.SelectedDate.GetValueOrDefault();
                string plec = "K";
                if (checkM.IsChecked == true)
                {
                    plec = "M";
                }
                if (checkM.IsChecked == true && checkW.IsChecked == true)
                {
                    throw new Exception("Please choose only one option!");
                }


                string idKlientaWybranego = listaCombo.SelectedItem.ToString();
                string[] temp = idKlientaWybranego.Split(' ');
                int index = Convert.ToInt32(temp[1]);
                MessageBox.Show(idKlientaWybranego);

                foreach (var item in context.Klienci.Where(x => x.idKlienta == index))
                {
                    if (dowod.Text.Trim() != "")
                    {
                        item.NrDowodu = dowod.Text;
                    }
                    if (nazwisko.Text.Trim() != "")
                    {
                        item.nazwiskoKlienta = nazwisko.Text;
                    }
                    if (imie.Text.Trim() != "")
                    {
                        item.imieKlienta = imie.Text;
                    }
                    if (ulica.Text.Trim() != "")
                    {
                        item.ulica = ulica.Text;
                    }
                    if (kod.Text.Trim() != "")
                    {
                        item.kodPocztowy = kod.Text;
                    }
                    if (miejscowosc.Text.Trim() != "")
                    {
                        item.Miejscowosc = miejscowosc.Text;
                    }
                    if (plec != item.plec)
                    {
                        item.plec = plec;
                    }

                    //void changedData(object sender, EventArgs e)
                    //{
                    //    dateChanged = true; //sprawdza czy data zosdtała zmieniona
                    //}
                    //if (dateChanged == true)
                    //{
                    //    item.dataUrodzenia = data;
                    //}
                    if (data != defaultDate)
                    {
                        item.dataUrodzenia = data;
                    }


                }




                void changedData(object sender, EventArgs e)
                {
                    dateChanged = true; //sprawdza czy data zosdtała zmieniona
                }
                context.SaveChanges();
                // string wynik = "";
                // wynik = klient.idKlienta + klient.imieKlienta + klient.nazwiskoKlienta + klient.NrDowodu;
                MessageBox.Show("Zedytowano klienta");

            }
        }
        
        

        private void UsuwanieKlientowFormularz()
        {
            panel1.Children.Clear();
            Label nowyKlient = new Label();
            nowyKlient.Content = "Input id";
            nowyKlient.FontSize = 15;
            panel1.Children.Add(nowyKlient);
            TextBox id = new TextBox();
            Thickness m = id.Margin;
            m.Top = -30;
            m.Left = -70;
            id.Margin = m;
            id.Width = 200;
            id.Height = 25;
            panel1.Children.Add(id);
            

            Button submit2 = new Button();

            submit2.Width = 80;
            submit2.Height = 20;
            submit2.Content = "Submit";
            panel1.Children.Add(submit2);



            submit2.Click += Submit;
            void Submit(object f, RoutedEventArgs g)
            {
               
                
                try
                {
                    int idKlientaDoUsuniecia = Convert.ToInt32(id.Text);
                    var query = context.Klienci.Where(x => x.idKlienta == idKlientaDoUsuniecia).Select(x => x).FirstOrDefault();
                    if (query != null)
                    {
                        MessageBoxResult result = MessageBox.Show($"You want to delete client with id = {query.idKlienta}, name ={query.imieKlienta + ' ' + query.nazwiskoKlienta} Please confirm to proceed.", "Confirm deletion", MessageBoxButton.YesNo);

                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                context.Klienci.Remove(query);
                                context.SaveChanges();
                                break;
                            case MessageBoxResult.No:
                                break;
                        }
                    }
                    else { MessageBox.Show("Nie ma takiego użytkownika"); };
                }
                catch (Exception e) {
                    MessageBox.Show("Nie ma takiego użytkownika"+e.Message.ToString());
                }
     



        }
        }


/// <summary>
/// Funkcja dodajaca klientow do bazy
/// </summary>
private void DodawanieUzytkownikowFormularz()
{

    //id
    Label nowyKlient = new Label();
    nowyKlient.Content = "Input id";
    nowyKlient.FontSize = 15;
    panel1.Children.Add(nowyKlient);
    TextBox id = new TextBox();
    Thickness m = id.Margin;
    m.Top = -30;
    m.Left = -70;
    id.Margin = m;
    id.Width = 200;
    id.Height = 25;
    panel1.Children.Add(id);

    //dowod
    Label dowodLabel = new Label();
    dowodLabel.Content = "Input id";
    dowodLabel.FontSize = 15;
    panel1.Children.Add(dowodLabel);
    TextBox dowod = new TextBox();
    dowod.Margin = m;
    dowod.Width = 200;
    dowod.Height = 25;
    panel1.Children.Add(dowod);

    //imie
    Label imieLabel = new Label();
    imieLabel.Content = "Input firstname";
    imieLabel.FontSize = 15;
    panel1.Children.Add(imieLabel);
    TextBox imie = new TextBox();
    imie.Width = 200;
    imie.Height = 25;
    imie.Margin = m;
    panel1.Children.Add(imie);

    //nazwisko
    Label nazwiskoLabel = new Label();
    nazwiskoLabel.Content = "Input surname";
    nazwiskoLabel.FontSize = 15;
    panel1.Children.Add(nazwiskoLabel);
    TextBox nazwisko = new TextBox();
    nazwisko.Width = 200;
    nazwisko.Height = 25;
    nazwisko.Margin = m;
    panel1.Children.Add(nazwisko);

    //checkbox
    Label checkboxLabel = new Label();
    checkboxLabel.Content = "Choose a gender";
    checkboxLabel.FontSize = 15;
    panel1.Children.Add(checkboxLabel);
    Thickness n = id.Margin;
    n.Top = -20;
    n.Left = 30;
    Thickness n2 = id.Margin;
    n2.Left = 200;
    n2.Top = -20;
    CheckBox checkW = new CheckBox();
    checkW.Width = 300;
    checkW.Margin = n;
    checkW.Height = 20;
    checkW.Name = "w";
    checkW.Content = "Women";

    CheckBox checkM = new CheckBox();
    checkM.Width = 300;
    checkM.Margin = n2;
    checkM.Height = 20;
    checkM.Name = "w";
    checkM.Content = "Men";

    panel1.Children.Add(checkW);
    panel1.Children.Add(checkM);


    //dataUr
    Thickness t = new Thickness();
    t.Left = -170;
    t.Top = -20;
    DateTime defaultDate = new DateTime(1999, 01, 01);
    Label dataLabel = new Label();
    dataLabel.Content = "Select birth date";
    dataLabel.FontSize = 15;
    panel1.Children.Add(dataLabel);
    DatePicker dataUr = new DatePicker();
    dataUr.SelectedDate = defaultDate;
    dataUr.Text = "Choose";

    dataUr.Width = 100;
    dataUr.Height = 25;
    dataUr.Margin = t;
    panel1.Children.Add(dataUr);

    //ulica
    Label ulicaLabel = new Label();
    ulicaLabel.Content = "Input adress";
    ulicaLabel.FontSize = 15;
    panel1.Children.Add(ulicaLabel);
    TextBox ulica = new TextBox();
    ulica.Width = 200;
    ulica.Height = 25;
    ulica.Margin = m;
    panel1.Children.Add(ulica);
    //kod pocztowy
    Label kodLabel = new Label();
    kodLabel.Content = "Postal code";
    kodLabel.FontSize = 15;
    panel1.Children.Add(kodLabel);
    Thickness kodT = new Thickness();
    kodT.Left = -210;
    kodT.Top = -30;
    TextBox kod = new TextBox();
    kod.Margin = kodT;
    kod.Width = 60;
    kod.Height = 25;
    panel1.Children.Add(kod);
    //miejscowosc
    Label miejscowoscL = new Label();
    miejscowoscL.Content = "Input city";
    miejscowoscL.FontSize = 15;
    panel1.Children.Add(miejscowoscL);
    TextBox miejscowosc = new TextBox();
    miejscowosc.Width = 200;
    miejscowosc.Height = 25;
    miejscowosc.Margin = m;
    panel1.Children.Add(miejscowosc);
    //submit
    Button submit2 = new Button();

    submit2.Width = 80;
    submit2.Height = 20;
    submit2.Content = "Submit";
    panel1.Children.Add(submit2);


   
          

    submit2.Click += Submit;
           

            // NEW ==============================================
            void Submit(object f, RoutedEventArgs g)
    {
                DateTime data = dataUr.SelectedDate.GetValueOrDefault();
                string plec = "K";
                if (checkM.IsChecked == true)
                {
                    plec = "M";
                }
                if (checkM.IsChecked == true && checkW.IsChecked == true)
                {
                    throw new Exception("Please choose only one option!");
                }

                Klienci klient = new Klienci()
        {
            idKlienta = Convert.ToInt32(id.Text),
            NrDowodu = dowod.Text,
            nazwiskoKlienta = nazwisko.Text,
            imieKlienta = imie.Text,
            plec = plec,
            dataUrodzenia = data,
            ulica = ulica.Text,
            kodPocztowy = kod.Text,
            Miejscowosc = miejscowosc.Text,
            dataWprowadzenia = DateTime.Now.Date
        };
        MessageBox.Show(data.Date.ToString());
                bool isFree = true;
                foreach (var item in context.Klienci.Select(x => x.idKlienta))
                {
                    if (item == Convert.ToInt32(id.Text))
                    {
                        isFree = false;
                        MessageBox.Show("User with same id already exists!");
                    }
                }

                if (isFree == true)
                {

                    context.Klienci.Add(klient);
                    context.SaveChanges();
                    // string wynik = "";
                    // wynik = klient.idKlienta + klient.imieKlienta + klient.nazwiskoKlienta + klient.NrDowodu;
                    MessageBox.Show("Dodano klienta");

                }

    }




}
       





        //Klienci klient = new Klienci()
        //{
        //    idKlienta = Convert.ToInt32(id.Text),
        //    imieKlienta = imie.Text,
        //    nazwiskoKlienta = nazwisko.Text


        //};
        // MessageBox.Show(id.Text+ imie.Text + nazwisko.Text);

    }
}


