using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        int index = 0;
        bool moreAuthors = false; 


        // bool dateChanged = false;


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
                case 4:
                    DodawanieKsiazki();
                    break;
                case 5:
                    EditOrders();
                    break;
                case 6:
                    DeleteUser deleteUser = new DeleteUser();
                    deleteUser.Show();
                    break;

            }


        }
        /// <summary>
        /// Edytuje wypozyczenia. Możliwość 
        /// </summary>
        private void EditOrders()
        {
            panel1.Children.Clear();
            //lista do wyboru wypozyczenia, ktore chcemy zedytowac
            Label wypozyczenieIDLabel = new Label();
            wypozyczenieIDLabel.Content = "Select order to edit";
            wypozyczenieIDLabel.FontSize = 15;
            panel1.Children.Add(wypozyczenieIDLabel);
            ComboBox OrdersList = new ComboBox();
            Thickness m = OrdersList.Margin;
            m.Top = -30;
            m.Left = -70;
            OrdersList.Margin = m;
            OrdersList.Width = 200;
            OrdersList.Height = 25;
            OrdersList.ItemsSource = context.Wypozyczenia.Select(x => x).ToList();
            panel1.Children.Add(OrdersList);

            //egzemplarz
            Label idEgzemplarzaLabel = new Label();
            idEgzemplarzaLabel.Content = "Input book(example) id";
            idEgzemplarzaLabel.FontSize = 15;
            panel1.Children.Add(idEgzemplarzaLabel);
            TextBox idEgzemplarza = new TextBox();
            idEgzemplarza.Margin = m;
            idEgzemplarza.Width = 200;
            idEgzemplarza.Height = 25;
            panel1.Children.Add(idEgzemplarza);

            //klient
            Label KlientId = new Label();
            KlientId.Content = "Choose client";
            KlientId.FontSize = 15;
            panel1.Children.Add(KlientId);
            ComboBox ClientsList = new ComboBox();
            ClientsList.Width = 200;
            ClientsList.Height = 25;
            ClientsList.Margin = m;
            ClientsList.ItemsSource = context.Klienci.Select(x => x).ToList();
            panel1.Children.Add(ClientsList);

            //oplata
            Label oplataZa7Dni = new Label();
            oplataZa7Dni.Content = "Input price to pay";
            oplataZa7Dni.FontSize = 15;
            panel1.Children.Add(oplataZa7Dni);
            TextBox nowaOplata = new TextBox();
            nowaOplata.Margin = m;
            nowaOplata.Width = 200;
            nowaOplata.Height = 25;
            panel1.Children.Add(nowaOplata);

            //data wypozyczenia
            DatePicker dataWypozyczenia = new DatePicker();

            dataWypozyczenia.Width = 100;
            dataWypozyczenia.Height = 25;

            panel1.Children.Add(dataWypozyczenia);
            //data zwrotu
            DatePicker dataZwrotu = new DatePicker();

            dataZwrotu.Width = 100;
            dataZwrotu.Height = 25;
            panel1.Children.Add(dataZwrotu);

            //submit button
            Button submit = new Button();
            submit.Width = 100;
            submit.Height = 50;
            panel1.Children.Add(submit);
            submit.Click += Submit_Click;

            void Submit_Click(object sender, RoutedEventArgs e)
            {
                string idWypozyczeniaCombox = OrdersList.SelectedItem.ToString();
                string[] temp = idWypozyczeniaCombox.Split(' ');
                int idWypozyczenia = Convert.ToInt32(temp[2]);





                //id klienta z listy



                foreach (var item in context.Wypozyczenia.Where(x => x.idWypozyczenia == idWypozyczenia))
                {
                    if (idEgzemplarza.Text != "")
                    {
                        item.idEgzemplarza = Convert.ToInt32(idEgzemplarza.Text);
                    }
                    else
                    {
                        item.idEgzemplarza = item.idEgzemplarza;
                    }
                    if (ClientsList.SelectedItem == null)
                    {
                        item.idKlienta = item.idKlienta;
                    }
                    else
                    {
                        string wybranyKlient = ClientsList.SelectedItem.ToString();
                        string[] temp2 = wybranyKlient.Split(' ');
                        int wybranyKlientId = Convert.ToInt32(temp2[1]);
                        int idKlienta = context.Klienci.Where(x => x.idKlienta == wybranyKlientId).Select(x => x.idKlienta).FirstOrDefault();
                        item.idKlienta = idKlienta;
                    }
                    if (dataWypozyczenia.SelectedDate != null)
                    {
                        item.dataWypozyczenia = dataWypozyczenia.SelectedDate.Value;
                    }
                    if (dataZwrotu.SelectedDate != null)
                    {
                        item.dataZwrotu = dataZwrotu.SelectedDate.Value;
                    }

                }
                MessageBox.Show("Zedytowano");
                context.SaveChanges();
                int ile = 0;
                //=======================Doplaty====================================
                //Doplata liczona jesli ktos przetrzyma ksiazke (wiecej niz 30 dni!)
                foreach (var item in context.Wypozyczenia.Where(x=>x.idWypozyczenia==idWypozyczenia).Select(x => new { x.idWypozyczenia, x.dataWypozyczenia, x.dataZwrotu, x.oplataZa7Dni }).Where(x => DbFunctions.DiffDays(x.dataWypozyczenia, x.dataZwrotu) > 30))
                {
                    DateConv dateC = new DateConv(item.dataWypozyczenia, (DateTime)item.dataZwrotu);
                    ile = dateC.IleDni - 30;

                    var doplataZaRodzaj = (decimal)item.oplataZa7Dni;
                    decimal doZaplatyObliczone = Math.Round((doplataZaRodzaj / 7) * ile,2);
                    if (doZaplatyObliczone != 0)
                    {
                        //tworze nowa doplate na podstawie wyzej obliczonych danych

                        bool exists = context.Doplaty.Any(t => t.idWypozyczenia == item.idWypozyczenia);
                        if (exists == true)
                        {
                            foreach (var doplata in context.Doplaty.Where(x => x.idWypozyczenia == item.idWypozyczenia))
                            {


                                if (doplata.doplata == null || doplata.doplata != doZaplatyObliczone)
                                {
                                    doplata.doplata = doZaplatyObliczone;
                                    MessageBox.Show($"Zamieniono doplate na{doZaplatyObliczone.ToString()}");

                                }
                            }
                        }
                        else
                        {
                            var doplataNowa = new Doplaty()
                            {
                                idWypozyczenia = item.idWypozyczenia,
                                doplata = doZaplatyObliczone

                            };
                            context.Doplaty.Add(doplataNowa);
                            MessageBox.Show($"Dodano dopłatę{doZaplatyObliczone.ToString()}");
                        }
                    }

                   
                }
                context.SaveChanges();

            }
           

        }

            


       

        private void DodawanieKsiazki()
        {
            panel1.Children.Clear();

            //id
            Label nowyEgzemplarz = new Label();
            nowyEgzemplarz.Content = "Input id";
            nowyEgzemplarz.FontSize = 15;
            panel1.Children.Add(nowyEgzemplarz);
            TextBox id = new TextBox();
            Thickness m = id.Margin;
            m.Top = -30;
            m.Left = -70;
            id.Margin = m;
            id.Width = 200;
            id.Height = 25;
            panel1.Children.Add(id);

            //dowod
            Label tytulLabel = new Label();
            tytulLabel.Content = "Input title";
            tytulLabel.FontSize = 15;
            panel1.Children.Add(tytulLabel);
            TextBox tytul = new TextBox();
            tytul.Margin = m;
            tytul.Width = 200;
            tytul.Height = 25;
            panel1.Children.Add(tytul);

            //rok wydania
            Label rokwydaniaLabel = new Label();
            rokwydaniaLabel.Content = "Input year";
            rokwydaniaLabel.FontSize = 15;
            panel1.Children.Add(rokwydaniaLabel);
            TextBox rokwydania = new TextBox();
            rokwydania.Width = 200;
            rokwydania.Height = 25;
            rokwydania.Margin = m;
            panel1.Children.Add(rokwydania);

            //rodzaj
            Label rodzajKsiazkiLabel = new Label();
            rodzajKsiazkiLabel.Content = "Choose type";
            rokwydaniaLabel.FontSize = 15;
            panel1.Children.Add(rodzajKsiazkiLabel);
            ComboBox rodzajKsiazki = new ComboBox();
            rodzajKsiazki.Margin = m;
            rodzajKsiazki.Width = 100;
            rodzajKsiazki.Height = 25;
            var queryRodzaje = context.Cennik.Select(x => x.rodzajKsiazki).ToList();
            rodzajKsiazki.ItemsSource = queryRodzaje;
            panel1.Children.Add(rodzajKsiazki);



            //gatunki
            ////multiselect!!!!
            ListBox list = new ListBox();
            list.SelectionMode = SelectionMode.Multiple;
            list.ItemsSource = context.gatunki.Select(x=>x.gatunek).ToList();
            list.MaxHeight = 150;
            panel1.Children.Add(list);
            //foreach (var item in list.items)
            //{
            //    If(list.Contains(item.ToString()))
            //  {
            //        list.SelectedValue = item.ToString();
            //    }
            //}

            StackPanel temp = new StackPanel();
           
            int idTworcyZComboBoxu = 0;
            Thickness n = new Thickness();

            n.Top = -2;
            n.Left = -70;
            ComboBox listaTworcowWBibliotece = new ComboBox();
            listaTworcowWBibliotece.Width = 300;
            listaTworcowWBibliotece.Height = 25;
            listaTworcowWBibliotece.Margin = n;
            listaTworcowWBibliotece.ItemsSource = context.Tworcy.Select(x => x).ToList();
            temp.Children.Add(listaTworcowWBibliotece);
            Label wiecejAutorowLabel = new Label();
            wiecejAutorowLabel.Content = "Write id of extra authors. If none - leave field empty.";
            temp.Children.Add(wiecejAutorowLabel);
            TextBox wiecejAutorow = new TextBox();
            wiecejAutorow.Width = 200;
            wiecejAutorow.Height = 25;
            temp.Children.Add(wiecejAutorow);

            //Wybor czy chcemy wybrac istniejacego tworce czy dodac nowego
            CheckBox checkYes = new CheckBox();
            checkYes.Click += checkBox1_Click;
            checkYes.Content = "New author";
            panel1.Children.Add(checkYes);
            panel1.Children.Add(temp);
           
           
           
           

        

            void checkBox1_Click(object sender, System.EventArgs e)
            {
                
                // The CheckBox control's Text property is changed each time the
                // control is clicked, indicating a checked or unchecked state.  
                if (checkYes.IsChecked == true)
                {
                   
                    temp.Children.Clear();
                    temp.Height = 80;
                    Button dodajAutora = new Button();
                    dodajAutora.Width = 100;
                    dodajAutora.Height = 30;
                    dodajAutora.Click += DodajAutora_Click;
                    dodajAutora.Content = "Add new author";
                    temp.Children.Add(dodajAutora);
                   
                }
               
                else if (checkYes.IsChecked == false)
                {
                    listaTworcowWBibliotece.ItemsSource = context.Tworcy.Select(x => x).ToList();
                    temp.Children.Clear();
                    temp.Height = 80;
                    temp.Children.Add(listaTworcowWBibliotece);
                    temp.Children.Add(wiecejAutorowLabel);
                    temp.Children.Add(wiecejAutorow);

                }
                

            } 
            Label LiczbaEgzemplarzyLabel = new Label();
            LiczbaEgzemplarzyLabel.Content = "Input number of examples";
            LiczbaEgzemplarzyLabel.FontSize = 15;
            panel1.Children.Add(LiczbaEgzemplarzyLabel);
            TextBox LiczbaEgzemplarzy = new TextBox();
            LiczbaEgzemplarzy.Width = 25;
            LiczbaEgzemplarzy.Height = 25;
            LiczbaEgzemplarzy.Margin = m;
            panel1.Children.Add(LiczbaEgzemplarzy);



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
                
                if (checkYes.IsChecked == false)
                {
                    string wybrany = listaTworcowWBibliotece.SelectedItem.ToString();
                    string[] pomocnicza = wybrany.Split(' ');
                    int idTworcy = Convert.ToInt32(pomocnicza[0]);

                    idTworcyZComboBoxu = idTworcy;
                }

                    int liczbaEgzemplarzyZ = 0;
                    if (LiczbaEgzemplarzy.Text == "")
                    {
                        liczbaEgzemplarzyZ = 0;
                    }
                    liczbaEgzemplarzyZ = Convert.ToInt32(LiczbaEgzemplarzy.Text);
                    index = Convert.ToInt32(id.Text);
                    panel1.Children.Clear();
          

                for (int i = 0; i < liczbaEgzemplarzyZ; i++)
                    {

                        TextBox idEgzemplarza = new TextBox();
                        idEgzemplarza.Width = 100;
                        idEgzemplarza.Height = 30;
                        panel1.Children.Add(idEgzemplarza);

                        Button submit = new Button();
                        submit.Height = 30;
                        submit.Width = 50;
                        submit.Content = "Submit";
                        panel1.Children.Add(submit);
                        submit.Click += Submit_Click;
                        void Submit_Click(object sender, RoutedEventArgs e)
                        {
                            Egzemplarze egzemplarz = new Egzemplarze()
                            {
                                idKsiazki = index,
                                idEgzemplarza = Convert.ToInt32(idEgzemplarza.Text)
                            };
                            context.Egzemplarze.Add(egzemplarz);
                            context.SaveChanges();

                        }
                
                    };
                    string rodzajWybrany = rodzajKsiazki.SelectedItem.ToString();
                    var queryCzyJestId = context.Ksiazki.Where(x => x.idKsiazki == index).Select(x => x.idKsiazki).FirstOrDefault();
                    int rokWydania = Convert.ToInt32(rokwydania.Text);
                var query = context.Tworcy.Where(x => x.idTworcy == idTworcyZComboBoxu).Select(x => x).ToList();
                List<Tworcy> listaTworcowDoDodania = new List<Tworcy>();
                if (wiecejAutorow.Text != "")
                {
                    string tekst = wiecejAutorow.Text;
                    List<int> listaAutorowId = new List<int>();
                    
                    string[] lista = tekst.Split(',');
                    foreach (var item in lista)
                    {
                        listaAutorowId.Add(Convert.ToInt32(item));
                    }
                    foreach (var item in listaAutorowId)
                    {
                        foreach (var tworca in context.Tworcy.Where(x=>x.idTworcy==item))
                        {
                            listaTworcowDoDodania.Add(tworca);
                        }
                    }
                }
                if (listaTworcowDoDodania.Count > 0)
                {
                    query.AddRange(listaTworcowDoDodania);
                }
                List<gatunki> gatunkiwybrane = new List<gatunki>();
                
                foreach (string item in list.SelectedItems)
                {
                    string itemGatunek = item.ToString();
                    foreach (var gatunek in context.gatunki.Select(x=>x))
                    {
                        string gatunekString = gatunek.ToString();
                        if (gatunekString == itemGatunek)
                        {
                            gatunkiwybrane.Add(gatunek);
                        }
                    }
                   
                }



                if (index == queryCzyJestId)
                {
                    MessageBox.Show("Book with the same id already exists");
                }
                else
                {
                    Ksiazki ksiazka = new Ksiazki()
                    {
                        idKsiazki = index,
                        tytulKsiazki = tytul.Text,
                        rokWydania = Convert.ToInt32(rokwydania.Text),
                        rodzajKsiazki = rodzajWybrany,
                        Tworcy = query,
                        gatunki=gatunkiwybrane
                    };
                    // ksiazka.Tworcy.Add(new Tworcy { idTworcy = idTworcyZComboBoxu });
                    context.Ksiazki.Add(ksiazka);
                    context.SaveChanges();

                    MessageBox.Show("Added");
                }
                //if (check2.IsChecked == true)
                //{
                //    Label dodawanieautorow = new Label();
                //    dodawanieautorow.Content = "Add more authors";
                //    Button dodawanieParuAutorow = new Button();
                //    dodawanieParuAutorow.Content = "Add more authors";
                //    panel1.Children.Add(dodawanieautorow);
                //    dodawanieautorow.MouseDoubleClick += DodajAutoraDoKsiazki;
                //    void DodajAutoraDoKsiazki(object sender, RoutedEventArgs e)
                //    {
                //        BookAuthor dodajAutora = new BookAuthor();
                //        dodajAutora.Show();

                //    }
                //}

            }


            void DodajEgzemplarz_Click(object sender, RoutedEventArgs e)
            {
                ExampleAdd dodajEgzemplarz = new ExampleAdd();
                dodajEgzemplarz.Show();
            }
        }

      

        private void DodajAutora_Click(object sender, RoutedEventArgs e)
        {
            AddAuthor dodajAutora = new AddAuthor();
            dodajAutora.Show();

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
            Thickness t2 = new Thickness();
            t2.Right = -300;
            t2.Top = -20;
            Button submit2 = new Button();
            submit2.Margin = t2;
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




                //void changedData(object sender, EventArgs e)
                //{
                //    dateChanged = true; //sprawdza czy data zosdtała zmieniona
                //}
                context.SaveChanges();
                // string wynik = "";
                // wynik = klient.idKlienta + klient.imieKlienta + klient.nazwiskoKlienta + klient.NrDowodu;
                MessageBox.Show("Zedytowano klienta");

            }
        }



        private void UsuwanieKlientowFormularz()
        {
            
            panel1.Children.Clear();

            Label listaLabel = new Label();
            listaLabel.Content = "List of clients. It's read-only!";
            listaLabel.FontSize = 15;
            panel1.Children.Add(listaLabel);
            List<Klienci> klienci = new List<Klienci>();

            foreach (var item in context.Klienci)
            {
                klienci.Add(item);
            }
            ComboBox listaCombo = new ComboBox();
            listaCombo.ItemsSource = klienci;
            listaCombo.IsReadOnly = true;
            listaCombo.SelectedIndex = 1;
            panel1.Children.Add(listaCombo);


            Label nowyKlient = new Label();
            nowyKlient.Content = "Input id";
            nowyKlient.FontSize = 15;
            panel1.Children.Add(nowyKlient);
            TextBox id = new TextBox();
            Thickness m = id.Margin;
            m.Top = -20;
            m.Left = -70;
            id.Margin = m;
            id.Width = 200;
            id.Height = 25;
            panel1.Children.Add(id);

            Thickness n = id.Margin;
            n.Left = -70;
            n.Top = 10;
           
            Button submit2 = new Button();
            submit2.Margin = n;
            submit2.Width = 120;
            submit2.Height = 30;
            var bc = new BrushConverter();
            submit2.Background = (Brush)bc.ConvertFrom("#FF12427C");
           submit2.Foreground=(Brush)bc.ConvertFrom("#FFFFFF");
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
                catch (Exception e)
                {
                    MessageBox.Show("Nie ma takiego użytkownika" + e.Message.ToString());
                }




            }
        }


        /// <summary>
        /// Funkcja dodajaca klientow do bazy
        /// </summary>
        private void DodawanieUzytkownikowFormularz()
        {
            panel1.Children.Clear();

            //readOnly lista klientow juz w bazie
            List<Klienci> klienci = new List<Klienci>();

            foreach (var item in context.Klienci)
            {
                klienci.Add(item);
            }
            ComboBox listaCombo = new ComboBox();
            listaCombo.ItemsSource = klienci;
            listaCombo.IsReadOnly = true;
            listaCombo.SelectedIndex = 1;
            panel1.Children.Add(listaCombo);
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
            dowodLabel.Content = "Input id card";
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


        private void UserButton_Checked(object sender, RoutedEventArgs e)
        {

            ShowContextMenu();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            // contentMenu.IsVisible=true;


        }

        private void ShowContextMenu()
        {
            var contextMenu = Resources["contentMenu"] as ContextMenu;
            contentMenu.IsOpen = true;

        }

        private void ContentMenuClick(object sender, RoutedEventArgs e)
        {

            var item = e.OriginalSource as MenuItem;
            if (item.Header.ToString() == "Log Out")
            {
                this.Close();
                LoginScreen login = new LoginScreen();
                login.Show();
            }
            if (item.Header.ToString() == "New User")
            {
                AddUser addUser = new AddUser();

                addUser.Show();

            }
        }

        }
}



