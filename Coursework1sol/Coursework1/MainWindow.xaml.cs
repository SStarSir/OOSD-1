using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
// line to add streamreader
using System.IO;


namespace Coursework1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            rdbNo.IsChecked = true;
        }
        
        public static bool studInt = false;
        public static bool validation = false;
        public int age = 0;
        public string email = "";
        





        //validation method

        public void validate(bool validation)

        {
            try
            {
                if (txtName.Text == "")
                {
                    throw new System.ArgumentException("Field cannot be blank", "Name");
                }

                if (txtSurname.Text == "")
                {
                    throw new System.ArgumentException("Field cannot be blank", "Surname");
                }

                //validation for age. first check if the field is not empty, then try to parse

                if (txtAge.Text == "")
                {
                    throw new System.ArgumentException("Field cannot be blank", "Age");
                }


                else if (txtAge.Text != "")

                {
                    int.TryParse(txtAge.Text, out age);

                    if (int.TryParse(txtAge.Text, out age))

                        age = Convert.ToInt32(txtAge.Text);
                    if ((age < 16) || (age > 101))
                    {
                        throw new System.ArgumentException("Age must be in the range 16 to 101", "Age");
                    }



                }






                if (cmoCourse.Text == "")
                {
                    throw new System.ArgumentException("Field cannot be blank", "Course");
                }

                if (txtAddress1.Text == "")
                {
                    throw new System.ArgumentException("Field cannot be blank", "Address 1");
                }

                if (txtAddress2.Text == "")
                {
                    throw new System.ArgumentException("Field cannot be blank", "Address 2");
                }

                if (txtCity.Text == "")
                {
                    throw new System.ArgumentException("Field cannot be blank", "City");
                }

                if (txtPostCode.Text == "")
                {
                    throw new System.ArgumentException("Field cannot be blank", "Postcode");
                }

                //validation for email. first checks if the field is not empty

                if (txtEmail.Text == "")
                {
                    throw new System.ArgumentException("Field cannot be blank", "Email");
                }

                else if (txtEmail.Text != "")



                {

                    email = txtEmail.Text;

                    if (email.Contains("@"))

                    {



                        char firstChar = email.ToCharArray().First();
                        Boolean isFirstCharacterLetter = char.IsLetter(firstChar) && (firstChar >= 'A' && firstChar <= 'Z') || (firstChar >= 'a' && firstChar <= 'z');
                        Boolean isFirstCharacterNumber = char.IsNumber(firstChar);


                        if ((isFirstCharacterLetter == false) & (isFirstCharacterNumber == false))



                        { throw new System.ArgumentException("Email field must start with letter or number"); }

                        char lastChar = email.ToCharArray().Last();

                        Boolean isLastCharacterLetter = char.IsLetter(lastChar) && (lastChar >= 'A' && lastChar <= 'Z') || (lastChar >= 'a' && lastChar <= 'z');
                        Boolean isLastCharacterNumber = char.IsNumber(lastChar);

                        if ((isLastCharacterLetter == false) & (isLastCharacterNumber == false))


                        { throw new System.ArgumentException("Email field must end with letter or number"); }

                    }

                    else

                    {
                        throw new System.ArgumentException("Email must contain @");
                    }
                }







                //country validation//

                if (rdbYes.IsChecked == false & rdbNo.IsChecked == false)

                { throw new System.ArgumentException("Field cannot be blank for international students", "International student?"); }


                if (cmoCountry.Text == "")
                {
                    if (studInt == true)

                    { throw new System.ArgumentException("Field cannot be blank for international students", "Country"); }
                }

                // message shown if everything validates succesfully

                validation = true;
                MessageBox.Show("Validated succesfully");
            }




            catch (ArgumentException ex)

            {
                validation = false;
                MessageBox.Show(ex.Message);
            }
           
        }
        
        //clear fields

        private void btnClear_Click(object sender, RoutedEventArgs e)


        {
            


            txtName.Clear();
            txtSurname.Clear();
            txtAge.Clear();
            cmoCourse.Text = "";
            txtAddress1.Clear();
            txtAddress2.Clear();
            txtCity.Clear();
            txtPostCode.Clear();
            txtEmail.Clear();
            cmoCountry.Text = "";

            studInt = false;
            age = 0;
            email = "";

            // this sets the rdNo to checked when reset button is pressed, so the country form disappears by default
            rdbNo.IsChecked = true;


        }

        private void rdbYes_Checked(object sender, RoutedEventArgs e)
        {
            studInt = true;



            lblCountry.Visibility = Visibility.Visible;
            cmoCountry.Visibility = Visibility.Visible;

        }



        private void rdbNo_Checked(object sender, RoutedEventArgs e)
        {
            studInt = false;

            lblCountry.Visibility = Visibility.Hidden;
            cmoCountry.Visibility = Visibility.Hidden;
        }

        private void btnValidate_Click(object sender, RoutedEventArgs e)
        {

            validate(validation);

        }

        

        
        // Used streamReader to load the long list of country using a .txt file located in debug. 
        // Using FileInfo should make the path absolute, preventing issues should the solution be moved.
        // I have added a try-catch to prevent breaking should the file not load.
        private void cmoCountry_Loaded(object sender, RoutedEventArgs e)
        {

            { try
                { 

            FileInfo file = new FileInfo("countrylist.txt");
                StreamReader sr = new StreamReader("countrylist.txt");

                string line = sr.ReadLine();

                while (line != null)
                {
                    cmoCountry.Items.Add(line);
                    line = sr.ReadLine();
                }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
