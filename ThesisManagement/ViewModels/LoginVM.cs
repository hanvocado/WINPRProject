using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security;
using System.Text.RegularExpressions;
using System.Windows;
using ThesisManagement.Repositories;
using ThesisManagement.Views.Professor;
using ThesisManagement.Views.Shared;
using ThesisManagement.Views.Student;

namespace ThesisManagement.ViewModels
{
    public class LoginVM : ViewModelBase
    {
        private readonly IUserRepository _userRepo;

        private string email;

        private bool isLoading = false;
        private string loadingVisibility = "Hidden";

        public bool IsLoading
        {
            get
            {
                return isLoading;
            }
            set
            {
                isLoading = value;
                if (value)
                    LoadingVisibility = "Visible";
                else
                    LoadingVisibility = "Hidden";
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public string LoadingVisibility
        {
            get
            {
                return loadingVisibility;
            }
            set
            {
                loadingVisibility = value;
                OnPropertyChanged(nameof(LoadingVisibility));
            }
        }

        [EmailAddress(ErrorMessage = "Bạn cần nhập email hợp lệ")]
        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
                ErrorMessage = "";
                OnPropertyChanged(nameof(Email));
            }
        }

        private SecureString password;

        [Required(ErrorMessage = "Mật khẩu không thể để trống")]
        public SecureString Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
                ErrorMessage = "";
                OnPropertyChanged(nameof(Password));
            }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }

            set
            {
                errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ViewModelCommand LoginCommand { get; }
        public LoginVM()
        {
            _userRepo = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLogin);
        }

        private bool CanExecuteLogin(object obj)
        {
            return !isLoading;
        }

        private void ExecuteLoginCommand(object obj)
        {
            IsLoading = true;
            if (ValidInfo())
            {
                var isUserValid = _userRepo.Authenticate(new NetworkCredential(email, password));
                if (isUserValid)
                {
                    var logicView = Application.Current.MainWindow as LoginView;
                    logicView.Hide();

                    if (SessionInfo.Role == Role.Student)
                    {
                        ShowStudentWindow();
                        return;
                    }
                    if (SessionInfo.Role == Role.Professor)
                    {
                        ShowProfessorWindow();
                        return;
                    }
                    if (SessionInfo.Role == Role.Admin)
                    {
                        Console.WriteLine();
                        return;
                    }
                }
            }
            ErrorMessage = "Thông tin đăng nhập không chính xác!";
            IsLoading = false;
        }

        private bool ValidInfo()
        {
            return Password != null && Password.Length > 0 && ValidEmail(email);
        }

        private bool ValidEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new System.Globalization.IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private void ShowProfessorWindow()
        {
            ProfessorMainView professorMainView = new();
            professorMainView.Show();
        }

        private void ShowStudentWindow()
        {
            StudentMainView wd = new();
            wd.Show();
        }
    }
}
