namespace LibraryManagementWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Properties
        enum SignState
        {
            SignIn,
            SignUp
        }
        SignState signState;
        User tempUser = new User()
        {
            Username = "",
            PasswordHash = ""
        };
        public User? ActiveUser { get; set; }
        public ObservableCollection<Book>? MyBooks { get; set; }
        public ObservableCollection<User>? MyUsers { get; set; } 
        public ObservableCollection<Review>? MyReviews { get; set; }
        public ObservableCollection<Loan>? MyLoans { get; set; }
        #endregion

        #region Methods
        void CreateDatabase()
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
            optionsBuilder.UseSqlServer("server = (localdb)\\MSSQLLocalDB; database = Library; Integrated Security = true");
            using var context = new LibraryContext(optionsBuilder.Options);
            context.Database.Migrate();
        }

        //security
        string GetHashPassword(string password)
        {
            SHA256 hash = SHA256.Create();
            var passwordBytes = Encoding.Default.GetBytes(password);
            var hashedPassword = hash.ComputeHash(passwordBytes);
            return Convert.ToHexString(hashedPassword);
        }
        
        //Book
        void AddBook(Book book)
        {
            using (var context = new LibraryContext())
            {
                context.Books.Add(book);
                context.SaveChanges();
            }
        }
        void UpdateBooksListView(ListView BooksListView)
        {
                using (var context = new LibraryContext())
                {
                    var Books = context.Books.ToList();
                    MyBooks = new ObservableCollection<Book>(Books);
                    BooksListView.ItemsSource = MyBooks;
                }
        }
        void EditBook(ListView BooksListView, Book book)
        {
            try
            {
                if (!BooksListView.HasItems)
                    throw new Exception("try to Add or Update Data\nListView Is Empty!");
                else
                {
                    using (var context = new LibraryContext())
                    {
                        if (!context.Books.Any(b => b.Author == book.Author && b.ISBN == book.ISBN && b.Title == book.Title))
                            throw new Exception("Book doesn't match try again!");
                        else
                        {
                            Book editableBook = context.Books.Single(b => b.Author == book.Author && b.ISBN == book.ISBN && b.Title == book.Title);
                            editableBook.Title = book.Title;
                            editableBook.Description = book.Description;
                            editableBook.Author = book.Author;
                            editableBook.ISBN = book.ISBN;
                            editableBook.PublishedDate = book.PublishedDate;
                            editableBook.CopiesAvailable++;
                            context.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        void DeleteBook(ListView BooksListView)
        {
            try
            {
                if (!BooksListView.HasItems)
                    throw new Exception("try to Add or Update Data\nListView Is Empty!");
                else if (BooksListView.SelectedItem == null)
                    throw new Exception("try to Select Book and try Again!\nNo Book are Selected!");
                else
                {
                    Book selectedBookInListView = (Book)BooksListView.SelectedItem;
                    using (var context = new LibraryContext())
                    {
                        Book selectedBook = context.Books.Single(b => b.BookId == selectedBookInListView.BookId);
                        context.Books.Remove(selectedBook);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        bool IsBookAlreadyExist(Book book)
        {
            using (var context = new LibraryContext())
            {
                return context.Books.Any(b => b.ISBN == book.ISBN && b.Title == book.Title && b.Author == book.Author);
            }
        }
        bool IsBookAlreadyExist(int bookId)
        {
            using (var context = new LibraryContext())
            {
                return context.Books.Any(b => b.BookId == bookId);
            }
        }
        int BookCopiesAvailable(Book book)
        {
            using (var context = new LibraryContext())
            {
                Book selectedBook = context.Books.Single(b => b.BookId == book.BookId);
                return selectedBook.CopiesAvailable;
            }
        }
        void BookDeleteCopy(Book book)
        {
            using (var context = new LibraryContext())
            {
                Book editableBook = context.Books.Single(b => b.BookId == book.BookId);
                editableBook.CopiesAvailable--;
                context.SaveChanges();
            }
        }

        //User
        void AddUser(User user)
        {
            using (var context = new LibraryContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
        void UpdateUsersListView(ListView UsersListView)
        {
            using (var context = new LibraryContext())
            {
                var Users = context.Users.ToList();
                MyUsers = new ObservableCollection<User>(Users);
                UsersListView.ItemsSource = MyUsers;
            }
        }
        void DeleteUser(ListView UsersListView)
        {
            if (!UsersListView.HasItems)
                throw new Exception("try to Add or Update Data\nListView Is Empty!");
            else if (UsersListView.SelectedItem == null)
                    throw new Exception("try to Select User and try Again!\nNo User are Selected!");
            else
            {
                using (var context = new LibraryContext())
                {
                    User selectedUserInListView = (User)UsersListView.SelectedItem;
                    User SelectedUser = context.Users.Single(u => u.UserId == selectedUserInListView.UserId);
                    context.Users.Remove(SelectedUser);
                    context.SaveChanges();
                
                }
            }
        }
        bool IsUserAlreadyExist(User user)
        {
            using (var context = new LibraryContext())
            {
                return context.Users.Any(u => u.Username == user.Username && u.PasswordHash == user.PasswordHash);
            }
        }
        bool IsUserAlreadyExist(int userID)
        {
            using (var context = new LibraryContext())
            {
                return context.Users.Any(u => u.UserId == userID);
            }
        }
        User SelectUser(User user)
        {
            using (var context = new LibraryContext())
            {
                return context.Users.Single(u => u.Username == user.Username && u.PasswordHash == user.PasswordHash);
            }
        }

        //Review
        void AddReview(Review review)
        {
            using (var context = new LibraryContext())
            {
                context.Reviews.Add(review);
                context.SaveChanges();
            }
        }
        void UpdateReviewListView(ListView ReviewListView)
        {
            using (var context = new LibraryContext())
            {
                    var Reviews = context.Reviews.ToList();
                    MyReviews = new ObservableCollection<Review>(Reviews);
                    ReviewListView.ItemsSource = MyReviews;
            }
        }
        void DeleteReview(ListView reviewListView)
        {
            try
            {
                if (!reviewListView.HasItems)
                    throw new Exception("try to Add or Update Data\nListView Is Empty!");
                else if (reviewListView.SelectedItem == null)
                    throw new Exception("try to Select Review and try Again!\nNo Review are Selected!");
                else
                {
                    using (var context = new LibraryContext())
                    {
                        Review selectedUserInListView = (Review)reviewListView.SelectedItem;

                        Review SelectedReview = context.Reviews.Single(r => r.ReviewId == selectedUserInListView.ReviewId);
                        context.Reviews.Remove(SelectedReview);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Loan
        void AddLoan(Loan loan)
        {
            using (var context = new LibraryContext())
            {
                context.Loans.Add(loan);
                context.SaveChanges();
            }
        }
        void UpdateLoanListView(ListView LoanListView)
        {
            using (var context = new LibraryContext())
            {
                var Loans = context.Loans.ToList();
                MyLoans = new ObservableCollection<Loan>(Loans);
                LoanListView.ItemsSource = MyLoans;
            }
        }
        void DeleteLoan(ListView loanListView)
        {
            try
            {
                if (!loanListView.HasItems)
                    throw new Exception("try to Add or Update Data\nListView Is Empty!");
                else if (loanListView.SelectedItem == null)
                    throw new Exception("try to Select Loan and try Again!\nNo Loan are Selected!");
                else
                {
                    using (var context = new LibraryContext())
                    {
                        Loan selectedLoanInListView = (Loan)loanListView.SelectedItem;

                        Loan SelectedLoan = context.Loans.Single(l => l.LoanId == selectedLoanInListView.LoanId);
                        context.Loans.Remove(SelectedLoan);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void UpdateLoan()
        {
            Loan loan = (Loan)LoansListView.SelectedItem;
            using (var context = new LibraryContext())
            {
                Loan selectedLoan = context.Loans.Single(l => l.LoanId.Equals(loan.LoanId));
                selectedLoan.ReturnDate = DateTime.Now;
                context.SaveChanges();
            }
            UpdateLoanListView(LoansListView);
        }


        //LoginForm
        void SignInSignUpWidthAnimation()
        {
            Storyboard SignInSignUpWidthAnimation = (Storyboard)FindResource("SignInSignUpWidthAnimation");
            BeginStoryboard(SignInSignUpWidthAnimation);
        }
        void SignInToSignUp()
        {
            Storyboard SignInToSignUp = (Storyboard)FindResource("SignInToSignUp");
            BeginStoryboard(SignInToSignUp);
        }
        void SignUpToSignIn()
        {
            Storyboard SignUpToSignIn = (Storyboard)FindResource("SignUpToSignIn");
            BeginStoryboard(SignUpToSignIn);
        }
        async void SignInSignUpTransition()
        {
            switch (signState)
            {
                case SignState.SignIn:
                    SignInToSignUp();
                    SignInSignUpWidthAnimation();
                    await Task.Run(() => Thread.Sleep(750));
                    HeaderLogginFormBorder.Text = "Join our Community!";
                    BodyLogginFormBorder.Text = "Welcome to LibraryApp!\nWe're excited to have you here.\nBy signing up, you'll gain access to exclusive features and vibrand community of like-minded individuals.";
                    SignInSignUp.Content = "Sign In";
                    SignInBorder.Visibility = Visibility.Collapsed;
                    SignUpBorder.Visibility = Visibility.Visible;
                    signState = SignState.SignUp;
                break;

                case SignState.SignUp:
                    SignInSignUpWidthAnimation();
                    SignUpToSignIn();
                    await Task.Run(() => Thread.Sleep(750));
                    HeaderLogginFormBorder.Text = "Welcome to Our Cummunity!";
                    BodyLogginFormBorder.Text = "Please log in to access your account and explore all the features we offer.\n\nif you're new, feel free to sign up and join us!";
                    SignInSignUp.Content = "Sign Up";
                    SignUpBorder.Visibility = Visibility.Collapsed;
                    SignInBorder.Visibility = Visibility.Visible;
                    signState = SignState.SignIn;
                break;
            }
        }
        void InvalidSignUpAnimations()
        {
            Storyboard storyboard1 = (Storyboard)FindResource("InvalidSignUpTextBlockAnimation");
            Storyboard storyboard = (Storyboard)FindResource("InvalidSignUpInputAnimation");
            storyboard1.Begin();
            storyboard.Begin();
        }
        User AddUserFromSignUpPage()
        {
            User user = new User()
            {
                Username = SignUpUserName.InnerText,
                PasswordHash = GetHashPassword(SignUpPassword.InnerText),
                Email = SignUpEmail.InnerText,
            };
            if (AdminRadioButton.IsChecked == true)
                user.Role = Role.Admin;
            else if (MemberRadioButton.IsChecked == true)
                user.Role = Role.Member;
            else
            {
                throw new Exception("no Role Selected");
            }
            return user;
        }
        void PageFadeAnimation(UIElement control)
        {
            Storyboard storyboard = new Storyboard();

            DoubleAnimation MoveAnimation = new DoubleAnimation()
            {
                By = -this.ActualHeight,
                Duration = TimeSpan.FromSeconds(1.5),
            };
            MoveAnimation.EasingFunction = new PowerEase()
            {
                Power = 5,
                EasingMode = EasingMode.EaseInOut
            };
            Storyboard.SetTarget(MoveAnimation, control);
            Storyboard.SetTargetProperty(MoveAnimation, new PropertyPath("(RenderTransform).(TranslateTransform.Y)"));



            var blurEffect = new BlurEffect()
            {
                Radius = 0
            };
            control.Effect = blurEffect;

            DoubleAnimation BlurAnimation = new DoubleAnimation()
            {
                By = 10,
                Duration = TimeSpan.FromSeconds(0.5)
            };
            Storyboard.SetTarget(BlurAnimation, control);
            Storyboard.SetTargetProperty(BlurAnimation, new PropertyPath("(Effect).(BlurEffect.Radius)"));



            DoubleAnimation OpacityAnimation = new DoubleAnimation()
            {
                BeginTime = TimeSpan.FromSeconds(0.5),
                To = 0,
                Duration = TimeSpan.FromSeconds(1)
            };
            Storyboard.SetTarget(OpacityAnimation, control);
            Storyboard.SetTargetProperty(OpacityAnimation, new PropertyPath("Opacity"));


            storyboard.Children.Add(MoveAnimation);
            storyboard.Children.Add(BlurAnimation);
            storyboard.Children.Add(OpacityAnimation);


            storyboard.Begin();
        }
        async void PageFadeAnimationNormal(params UIElement[] element)
        {
            foreach (UIElement control in element)
            {
                Storyboard storyboard = new Storyboard();

                DoubleAnimation MoveAnimation = new DoubleAnimation()
                {
                    From = 0,
                    To = -this.ActualHeight * 2,
                    Duration = TimeSpan.FromSeconds(1.5),
                };
                MoveAnimation.EasingFunction = new PowerEase()
                {
                    Power = 5,
                    EasingMode = EasingMode.EaseInOut
                };
                Storyboard.SetTarget(MoveAnimation, control);
                Storyboard.SetTargetProperty(MoveAnimation, new PropertyPath("(RenderTransform).(TranslateTransform.Y)"));



                DoubleAnimation OpacityAnimation = new DoubleAnimation()
                {
                    BeginTime = TimeSpan.FromSeconds(0.5),
                    From = 1,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(1)
                };
                Storyboard.SetTarget(OpacityAnimation, control);
                Storyboard.SetTargetProperty(OpacityAnimation, new PropertyPath("Opacity"));


                storyboard.Children.Add(MoveAnimation);
                storyboard.Children.Add(OpacityAnimation);


                storyboard.Begin();
            }
            await Task.Delay(1500);
            foreach (UIElement control in element)
            {
                control.Visibility = Visibility.Collapsed;
            }
        }
        void PageAppearAnimation(UIElement control)
        {
            
            Storyboard storyboard = new Storyboard();

            DoubleAnimation MoveAnimation = new DoubleAnimation()
            {
                From = this.ActualHeight*2,
                To = 0,
                Duration = TimeSpan.FromSeconds(1.5),
            };
            MoveAnimation.EasingFunction = new PowerEase()
            {
                Power = 5,
                EasingMode = EasingMode.EaseInOut
            };
            Storyboard.SetTarget(MoveAnimation, control);
            Storyboard.SetTargetProperty(MoveAnimation, new PropertyPath("(RenderTransform).(TranslateTransform.Y)"));



            DoubleAnimation OpacityAnimation = new DoubleAnimation()
            {
                BeginTime = TimeSpan.FromSeconds(0.5),
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1)
            };
            Storyboard.SetTarget(OpacityAnimation, control);
            Storyboard.SetTargetProperty(OpacityAnimation, new PropertyPath("Opacity"));


            storyboard.Children.Add(MoveAnimation);
            storyboard.Children.Add(OpacityAnimation);


            storyboard.Begin();
        }

        //MainMenu
        void MainMenuAppear()
        {
            RoundedMenu.Visibility = Visibility.Visible;
            Storyboard storyboard = (Storyboard)FindResource("RoundedMenuStoryboardAppear");
            storyboard.Begin();
        }
        void MainPageApprear()
        {
            Storyboard MainPageAppear = (Storyboard)FindResource("MainPageAppear");
            Storyboard MainPageTextAppear = (Storyboard)FindResource("MainPageTextsAppear");

            MainPageAppear.Begin();
            MainPageTextAppear.Begin();
        }
        void UnselectButtonsVisual()
        {
            Color color = (Color)ColorConverter.ConvertFromString("#0A5F83");

            UsersButton.Background = new SolidColorBrush(Colors.Transparent);
            UsersButton.Foreground = new SolidColorBrush(color);

            BooksButton.Background = new SolidColorBrush(Colors.Transparent);
            BooksButton.Foreground = new SolidColorBrush(color);

            LoansButton.Background = new SolidColorBrush(Colors.Transparent);
            LoansButton.Foreground = new SolidColorBrush(color);

            ReviewsButton.Background = new SolidColorBrush(Colors.Transparent);
            ReviewsButton.Foreground = new SolidColorBrush(color);

        }

        //BookMenu
        void InvalidBookInputAnimation()
        {
            Storyboard InvalidBookInputAnimation = (Storyboard)FindResource("InvalidBookInputAnimation");
            InvalidBookInputAnimation.Begin();
        }

        //UserMenu
        void UserPageInvalidAnimation()
        {
            Storyboard storyboard = (Storyboard)FindResource("InvalidUserInputAnimation");
            storyboard.Begin();
        }

        //LoanMenu
        void InvalidLoanInputAnimation()
        {
            Storyboard storyboard = (Storyboard)FindResource("InvalidLoanInputAnimation");
            storyboard.Begin();
        }

        //ReviewMenu
        void InvalidReviewInputAnimation()
        {
            Storyboard storyboard = (Storyboard)FindResource("InvalidReviewInputAnimation");
            storyboard.Begin();
        }
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            //ensure Database Created In Users Sistem
            CreateDatabase();
            
            //Load Database first for Lagless User Experience
            Task.Run(() => IsUserAlreadyExist(tempUser));
        }

        private void SignInSignUp_Click(object sender, RoutedEventArgs e)
        {
            SignInSignUpTransition();
        }

        private async void SignIn_Click(object sender, RoutedEventArgs e)
        {

            User user = new User()
            {
                Username = SignInUserName.InnerText,
                PasswordHash = GetHashPassword(SignInPassword.InnerText)
            };

            if (IsUserAlreadyExist(user))
            {
                PageFadeAnimation(SignUpSignInBorder);
                MainMenuAppear();
                ActiveUser = SelectUser(user);
                MainPageApprear();
                //MessageBox.Show($"ID: {ActiveUser.UserId}\nUsername: {ActiveUser.Username}\nPasswordHash: {ActiveUser.PasswordHash}\nEmail: {ActiveUser.Email}\nRole: {ActiveUser.Role}");
                //await Task.Delay(750);

                MainPage.Visibility = Visibility.Visible;


                await Task.Delay(1500);
                SignUpSignInBorder.Visibility = Visibility.Collapsed;
            }
            else
            {
                Storyboard storyboard1 = (Storyboard)FindResource("InvalidSignInInputAnimation");
                Storyboard storyboard = (Storyboard)FindResource("InvalidSignInTextBlockAnimation");
                storyboard1.Begin();
                storyboard.Begin();
            }
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            bool roleSelected = AdminRadioButton.IsChecked == true || MemberRadioButton.IsChecked == true;
            if (string.IsNullOrWhiteSpace(SignUpUserName.InnerText) || string.IsNullOrWhiteSpace(SignUpPassword.InnerText) || string.IsNullOrWhiteSpace(SignUpEmail.InnerText) || !roleSelected)
            {
                InvalidSignUpTextBlock.Text = "Fill All the Fields and Try Again!";

                InvalidSignUpAnimations();
                
            }
            else
            {
                User user = AddUserFromSignUpPage();
                if (IsUserAlreadyExist(user))
                {
                    InvalidSignUpTextBlock.Text = "This user is Already Exist! Change the properties.";
                    InvalidSignUpAnimations();
                }
                else
                {
                    AddUser(user);
                    MessageBox.Show("SignUp Process Completed.");
                    SignInSignUpTransition();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            Book book = new Book()
            {
                Title = TitleTextBox.InnerText,
                Description = DescriptionTextBox.InnerText,
                Author = AuthorTextBox.InnerText,
                ISBN = ISBNTextBox.InnerText,
                PublishedDate = DateTime.Now,
                CopiesAvailable = 1
            };

            bool isTextboxesEmpty = string.IsNullOrWhiteSpace(TitleTextBox.InnerText) || string.IsNullOrWhiteSpace(AuthorTextBox.InnerText) || string.IsNullOrWhiteSpace(ISBNTextBox.InnerText);

            if (isTextboxesEmpty)
            {
                InvalidBookTextBlock.Text = "Fill the Fields and try again.";
                InvalidBookInputAnimation();
            }
            else
            {
                if (IsBookAlreadyExist(book))
                {
                    EditBook(BooksListView, book);
                    UpdateBooksListView(BooksListView);
                }
                else
                {
                    AddBook(book);
                    UpdateBooksListView(BooksListView);
                }
            }
        }

        private void DeleteBookButton_Click(object sender, RoutedEventArgs e)
        {
            Book book = new Book()
            {
                Title = TitleTextBox.InnerText,
                Description = DescriptionTextBox.InnerText,
                Author = AuthorTextBox.InnerText,
                ISBN = ISBNTextBox.InnerText,
                PublishedDate = DateTime.Now,
                CopiesAvailable = 1
            };

            if (!BooksListView.HasItems)
                MessageBox.Show("ListView Is Empty.");
            else
            {
                if (BooksListView.SelectedItem == null)
                    MessageBox.Show("Select an item first.");
                else
                {
                    if (BookCopiesAvailable((Book)BooksListView.SelectedItem) > 1)
                        BookDeleteCopy((Book)BooksListView.SelectedItem);
                    else
                        DeleteBook(BooksListView);
                }
            }
            UpdateBooksListView(BooksListView);
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteUser(UsersListView);
            UpdateUsersListView(UsersListView);
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            User user = new User()
            {
                Username = UsernameTextBox.InnerText,
                PasswordHash = GetHashPassword(PasswordTextBox.InnerText),
                Email = EmailTextBox.InnerText
            };
            if (UserAdminRadioButton.IsChecked == true)
                user.Role = Role.Admin;
            else if (UserMemberRadioButton.IsChecked == true)
                user.Role = Role.Member;


            bool roleSelected = UserAdminRadioButton.IsChecked == true || UserMemberRadioButton.IsChecked == true;
            if (string.IsNullOrWhiteSpace(UsernameTextBox.InnerText) || string.IsNullOrWhiteSpace(PasswordTextBox.InnerText) || string.IsNullOrWhiteSpace(EmailTextBox.InnerText) || !roleSelected)
            {
                InvalidUserTextBlock.Text = "Fill All the Fields and Try Again!";
                UserPageInvalidAnimation();
                
                //InvalidSignUpAnimations();

            }
            else
            {
                if (IsUserAlreadyExist(user))
                {
                    InvalidUserTextBlock.Text = "This user is Already Exist! Change the properties.";
                    UserPageInvalidAnimation();


                    //InvalidSignUpAnimations();
                }
                else
                {
                    AddUser(user);
                    UpdateUsersListView(UsersListView);
                    //SignInSignUpTransition();
                }
            }
        }

        private void AddLoanButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(LoanUserIdTextBox.InnerText) || string.IsNullOrWhiteSpace(LoanBookIdTextBox.InnerText))
                    throw new Exception("Fill the Required Fields!");
                else if (!IsUserAlreadyExist(Convert.ToInt32(LoanUserIdTextBox.InnerText)) || !IsBookAlreadyExist(Convert.ToInt32(LoanBookIdTextBox.InnerText)))
                        throw new Exception("User ID or Book ID is not found!");
                else
                {
                    Loan loan = new Loan()
                    {
                        BookId = Convert.ToInt32(LoanBookIdTextBox.InnerText),
                        UserId = Convert.ToInt32(LoanUserIdTextBox.InnerText),
                        LoanDate = DateTime.Now
                    };
                    AddLoan(loan);
                    UpdateLoanListView(LoansListView);
                }
            }
            catch (Exception ex)
            {
                InvalidLoanTextBlock.Text = ex.Message;
                InvalidLoanInputAnimation();
            }
            
        }
        
        private void LoanDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteLoan(LoansListView);
            UpdateLoanListView(LoansListView);
        }

        private void LoanReturnedButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoansListView.Items == null)
                MessageBox.Show("Try to Add Loan Fist");
            else if (LoansListView.SelectedItem == null)
                    MessageBox.Show("Try to Select An Item!");
            else
                UpdateLoan();     
        }

        private void AddReviewButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ReviewUserIdTextBox.InnerText) || string.IsNullOrWhiteSpace(ReviewBookIdTextBox.InnerText) || string.IsNullOrWhiteSpace(ReviewRatingTextBox.InnerText))
                    throw new Exception("Fill the Required Fields!");
                else if (Convert.ToInt32(ReviewRatingTextBox.InnerText) < 0 || Convert.ToInt32(ReviewRatingTextBox.InnerText) > 5)
                        throw new Exception("insert Rating Range between (0) and (5)");
                else if (!IsUserAlreadyExist(Convert.ToInt32(ReviewUserIdTextBox.InnerText)) || !IsBookAlreadyExist(Convert.ToInt32(ReviewBookIdTextBox.InnerText)))
                        throw new Exception("User ID or Book ID is not found!");
                else
                {
                    Review review = new Review()
                    {
                        UserId = Convert.ToInt32(ReviewUserIdTextBox.InnerText),
                        BookId = Convert.ToInt32(ReviewBookIdTextBox.InnerText),
                        Rating = Convert.ToInt32(ReviewRatingTextBox.InnerText),
                        Comment = ReviewCommentTextBox.InnerText,
                        ReviewDate = DateTime.Now
                    };
                    AddReview(review);
                    UpdateReviewListView(ReviewsListView);
                }
            }
            catch (Exception ex)
            {
                InvalidReviewTextBlock.Text = ex.Message;
                InvalidReviewInputAnimation();
            }
        }

        private void DeleteReviewButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteReview(ReviewsListView);
            UpdateReviewListView(ReviewsListView);
        }

        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersPage.IsVisible)
                return;
            else
            {
                UnselectButtonsVisual();
                PageFadeAnimationNormal(MainPage, BooksPage, LoanssPage, ReviewsPage);
                PageAppearAnimation(UsersPage);
                UsersPage.Visibility = Visibility.Visible;
                UpdateUsersListView(UsersListView);
            }
        }

        private void BooksButton_Click(object sender, RoutedEventArgs e)
        {
            if (BooksPage.IsVisible)
                return;
            else
            {
                UnselectButtonsVisual();
                PageFadeAnimationNormal(MainPage, UsersPage, LoanssPage, ReviewsPage);
                PageAppearAnimation(BooksPage);
                BooksPage.Visibility = Visibility.Visible;
                UpdateBooksListView(BooksListView);
            }
        }

        private void LoansButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoanssPage.IsVisible)
                return;
            else
            {
                UnselectButtonsVisual();
                PageFadeAnimationNormal(MainPage, BooksPage, UsersPage, ReviewsPage);
                PageAppearAnimation(LoanssPage);
                LoanssPage.Visibility = Visibility.Visible;
                UpdateLoanListView(LoansListView);
            }
        }

        private void ReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            if (ReviewsPage.IsVisible)
                return;
            else
            {
                UnselectButtonsVisual();
                PageFadeAnimationNormal(MainPage, BooksPage, UsersPage, LoanssPage);
                PageAppearAnimation(ReviewsPage);
                ReviewsPage.Visibility = Visibility.Visible;
                UpdateReviewListView(ReviewsListView);
            }
        }

        private void DragMoveBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}