using InventoryApp.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace InventoryApp.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private Visibility loginVis;
        public Visibility LoginVis 
        {
            get { return loginVis; }
            set 
            { 
                loginVis = value;
                OnPropertyChanged("LoginVis");
            } 
        }

        private Visibility registerVis;
        public Visibility RegisterVis
        {
            get { return registerVis; }
            set 
            { 
                registerVis = value;
                OnPropertyChanged("RegisterVis");
            }
        }
        private string buttonContent;
        public string ButtonContent
        {
            get { return buttonContent; }
            set 
            { 
                buttonContent = value;
                OnPropertyChanged("ButtonContent");
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set 
            { 
                email = value;
                OnPropertyChanged("Email");
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set 
            { 
                password = value;
                OnPropertyChanged("Password");
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set 
            { 
                confirmPassword = value;
                OnPropertyChanged("ConfirmPassword");
            }
        }

        public ShowLoginRegisterFocusCommand ShowLoginRegisterFocusCommand { get; set; }



        public LoginViewModel()
        {
            ShowLoginRegisterFocusCommand = new ShowLoginRegisterFocusCommand(this);

            LoginVis = Visibility.Visible;
            RegisterVis = Visibility.Collapsed;
            ButtonContent = "Register";
        }


        public void ChangeFocus()
        {
            if(LoginVis == Visibility.Visible)
            {
                LoginVis = Visibility.Collapsed;
                RegisterVis = Visibility.Visible;
                ButtonContent = "Login";
            }
            else
            {
                LoginVis = Visibility.Visible;
                RegisterVis = Visibility.Collapsed;
                ButtonContent = "Register";
            }
        }
    }
}
