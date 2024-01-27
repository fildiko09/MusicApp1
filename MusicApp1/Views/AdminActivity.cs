using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.View;
using AndroidX.DrawerLayout.Widget;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Navigation;
using Google.Android.Material.Snackbar;
using MusicApp1.Models.BLL;
using MusicApp1.Models.DAL;
using MusicApp1.Models.Entities;
using MusicApp1.ViewModels;
using SQLite;
using Xamarin.Essentials;

namespace MusicApp1.Views
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class AdminActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        string[] items;
        ListView mainList;
        TopicTable[] topics;
        QuizTable[] quizzes;
        QuestionTable[] questions;
        RequestTable[] requests;

        AdminVM adminVM;

        public AdminActivity()
        {
            adminVM = new AdminVM();
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetBaseView();

        }

        private void SetBaseView()
        {
            adminVM.CreateTables();

            topics = adminVM.GetAllTopics();
            quizzes = adminVM.GetAllQuizzes();
            questions = adminVM.GetAllQuestions();
            requests = adminVM.GetAllRequests();

            SetContentView(Resource.Layout.Admin);
            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            MainListSettings();
        }

        private void MainListSettings()
        {
            string myPref = Preferences.Get("default_view", "default");
            if (myPref == "topics")
            {
                items = (from o in topics select o.topicName).ToArray();
            }
            else if (myPref == "quizzes")
            {
                items = (from o in quizzes select o.quizName).ToArray();
            }
            else if (myPref == "questions")
            {
                items = (from o in questions select o.question).ToArray();
            }
            else if(myPref == "requests")
            {
                items = (from o in requests select o.requestDescription).ToArray();
            }

            mainList = (ListView)FindViewById<ListView>(Resource.Id.historyListView);
            mainList.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, items);

            mainList.ItemClick += ItemOnClick;
        }

        private void ItemOnClick(object sender, AdapterView.ItemClickEventArgs eventArgs)
        {
            string myPref = Preferences.Get("default_view", "default");

            if (myPref == "topics")
            {
                TopicUpdate(sender, eventArgs);
            }
            else if (myPref == "quizzes")
            {
                QuizUpdate(sender, eventArgs);
            }
            else if (myPref == "questions")
            {
                QuestionUpdate(sender, eventArgs);
            }
            else if(myPref=="requests")
            {
                RequestSee(sender, eventArgs);
            }
        }

        RequestTable selectReq;
        private void RequestSee(object sender, AdapterView.ItemClickEventArgs eventArgs)
        {
            SetContentView(Resource.Layout.SeeRequest);
            selectReq = requests[eventArgs.Position];
            TextView reqDesc = FindViewById<TextView>(Resource.Id.request1);
            TextView idU = FindViewById<TextView>(Resource.Id.request2);

            reqDesc.Text = selectReq.requestDescription;
            idU.Text = selectReq.loginIdFK.ToString();

            var btnDeleteReq = FindViewById<FloatingActionButton>(Resource.Id.btnDeleteReq);
            btnDeleteReq.Click += BtnDeleteReq_Click;

            var btnBackReq = FindViewById<FloatingActionButton>(Resource.Id.btnBackReq);
            btnBackReq.Click += BtnBackReq_Click;

        }

        private void BtnDeleteReq_Click(object sender, EventArgs e)
        {
            try
            {
                adminVM.DeleteRequest(selectReq);
                Toast.MakeText(this, "Request Deleted…", ToastLength.Long).Show();
                SetBaseView();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
        }

        private void BtnBackReq_Click(object sender, EventArgs e)
        {
            try
            {
                SetBaseView();
            }
            catch(Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
        }

        EditText name;
        EditText description;
        string category;
        TopicTable selectItem;

        private void TopicUpdate(object sender, AdapterView.ItemClickEventArgs eventArgs)
        {
            SetContentView(Resource.Layout.UpdateTopic);
            selectItem = topics[eventArgs.Position];
            name = FindViewById<EditText>(Resource.Id.updateTopicName);
            name.Text = selectItem.topicName;
            description = FindViewById<EditText>(Resource.Id.updateTopicDescription);
            description.Text = selectItem.description;
            category = selectItem.category;
            TextView txtCat = FindViewById<TextView>(Resource.Id.category);
            txtCat.Text = "Topic's category is: " + category;
            Spinner spinner = FindViewById<Spinner>(Resource.Id.updateCategory);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);

            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.categories_array2, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            var btnUpdate = FindViewById<FloatingActionButton>(Resource.Id.btnUpdateTopic);
            btnUpdate.Click += update_Click;

            var btnDelete = FindViewById<FloatingActionButton>(Resource.Id.btnDeleteTopic);
            btnDelete.Click += delete_Click;

            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string newCategory = spinner.GetItemAtPosition(e.Position).ToString();
            if (newCategory != string.Empty)
            {
                category = newCategory;
            }
        }

        private void update_Click(object sender, EventArgs e)
        {
            try
            {
                TopicTable table = new TopicTable();
                table.topicId = selectItem.topicId;
                table.topicName = name.Text;
                table.description = description.Text;
                table.category = category;
                adminVM.UpdateTopic(table);
                Toast.MakeText(this, "Topic Updated…", ToastLength.Long).Show();
                SetBaseView();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                adminVM.DeleteTopic(selectItem);
                Toast.MakeText(this, "Topic Deleted…", ToastLength.Long).Show();
                SetBaseView();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
        }

        EditText nameQuiz;
        TopicTable myTopic;
        QuizTable selectQuizItem;
        private void QuizUpdate(object sender, AdapterView.ItemClickEventArgs eventArgs)
        {
            string quizTopic;
            SetContentView(Resource.Layout.UpdateQuiz);
            selectQuizItem = quizzes[eventArgs.Position];
            nameQuiz = FindViewById<EditText>(Resource.Id.updateQuizName);
            nameQuiz.Text = selectQuizItem.quizName;
            myTopic = adminVM.GetTopicByID(selectQuizItem.topicIdFK);
            quizTopic = myTopic.topicName;
            TextView txtTop = FindViewById<TextView>(Resource.Id.topicFK);
            txtTop.Text = "Belongs to topic: " + quizTopic;
            Spinner spinnerTopic = FindViewById<Spinner>(Resource.Id.updateTopicFK);

            spinnerTopic.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerTopic_ItemSelected);

            List<string> list = new List<string>();
            list.Add("");
            for (int i = 0; i < topics.Length; i++)
            {
                list.Add(topics[i].topicName);
            }
            string[] spinnerTopicItems = list.ToArray();

            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, spinnerTopicItems);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerTopic.Adapter = adapter;

            var btnUpdateQ = FindViewById<FloatingActionButton>(Resource.Id.btnUpdateQuiz);
            btnUpdateQ.Click += updateQuiz_Click;

            var btnDeleteQ = FindViewById<FloatingActionButton>(Resource.Id.btnDeleteQuiz);
            btnDeleteQ.Click += deleteQuiz_Click;

            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
        }

        private void spinnerTopic_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string newTopic = spinner.GetItemAtPosition(e.Position).ToString();
            if (newTopic != string.Empty)
            {
                myTopic = topics[e.Position - 1];
            }
        }

        private void updateQuiz_Click(object sender, EventArgs e)
        {
            try
            {
                QuizTable table = new QuizTable();
                table.quizId = selectQuizItem.quizId;
                table.quizName = nameQuiz.Text;
                table.topicIdFK = myTopic.topicId;
                adminVM.UpdateQuiz(table);
                Toast.MakeText(this, "Quiz Updated…", ToastLength.Long).Show();
                SetBaseView();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
        }

        private void deleteQuiz_Click(object sender, EventArgs e)
        {
            try
            {
                adminVM.DeleteQuiz(selectQuizItem);
                Toast.MakeText(this, "Quiz Deleted…", ToastLength.Long).Show();
                SetBaseView();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
        }

        EditText txtQuestion;
        EditText txtAnswerA;
        EditText txtAnswerB;
        EditText txtAnswerC;
        EditText txtCorrectAnswer;
        QuizTable myQuiz;
        QuestionTable selectQuestionItem;

        private void QuestionUpdate(object sender, AdapterView.ItemClickEventArgs eventArgs)
        {
            string questionQuiz;
            SetContentView(Resource.Layout.UpdateQuestion);
            selectQuestionItem = questions[eventArgs.Position];
            txtQuestion = FindViewById<EditText>(Resource.Id.updateYourQuestion);
            txtQuestion.Text = selectQuestionItem.question;
            txtAnswerA = FindViewById<EditText>(Resource.Id.updateAnswerA);
            txtAnswerA.Text = selectQuestionItem.answerA;
            txtAnswerB = FindViewById<EditText>(Resource.Id.updateAnswerB);
            txtAnswerB.Text = selectQuestionItem.answerB;
            txtAnswerC = FindViewById<EditText>(Resource.Id.updateAnswerC);
            txtAnswerC.Text = selectQuestionItem.answerC;
            txtCorrectAnswer = FindViewById<EditText>(Resource.Id.updateCorrectAnswer);
            txtCorrectAnswer.Text = selectQuestionItem.correctAnswer;
            myQuiz = adminVM.GetQuizByID(selectQuestionItem.quizIdFK);
            questionQuiz = myQuiz.quizName;
            TextView txtQuiz = FindViewById<TextView>(Resource.Id.quizFK);
            txtQuiz.Text = "Belongs to quiz: " + questionQuiz;
            Spinner spinnerQuiz = FindViewById<Spinner>(Resource.Id.updateQuizFK);

            spinnerQuiz.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerQuiz_ItemSelected);

            List<string> list = new List<string>();
            list.Add("");
            for (int i = 0; i < quizzes.Length; i++)
            {
                list.Add(quizzes[i].quizName);
            }
            string[] spinnerQuizItems = list.ToArray();

            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, spinnerQuizItems);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerQuiz.Adapter = adapter;

            var btnUpdateQue = FindViewById<FloatingActionButton>(Resource.Id.btnUpdateQuestion);
            btnUpdateQue.Click += updateQuestion_Click;

            var btnDeleteQue = FindViewById<FloatingActionButton>(Resource.Id.btnDeleteQuestion);
            btnDeleteQue.Click += deleteQuestion_Click;

            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
        }

        private void deleteQuestion_Click(object sender, EventArgs e)
        {
            try
            {
                adminVM.DeleteQuestion(selectQuestionItem);
                Toast.MakeText(this, "Question Deleted…", ToastLength.Long).Show();
                SetBaseView();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
        }

        private void updateQuestion_Click(object sender, EventArgs e)
        {
            try
            {
                QuestionTable table = new QuestionTable();
                table.questionId = selectQuestionItem.questionId;
                table.question = txtQuestion.Text;
                table.answerA = txtAnswerA.Text;
                table.answerB = txtAnswerB.Text;
                table.answerC = txtAnswerC.Text;
                table.correctAnswer = txtCorrectAnswer.Text;
                table.quizIdFK = myQuiz.quizId;
                adminVM.UpdateQuestion(table);
                Toast.MakeText(this, "Question Updated…", ToastLength.Long).Show();
                SetBaseView();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
        }

        private void spinnerQuiz_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string newQuiz = spinner.GetItemAtPosition(e.Position).ToString();
            if (newQuiz != string.Empty)
            {
                myQuiz = quizzes[e.Position - 1];
            }
        }

        public override void OnBackPressed()
        {

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if (drawer != null)
            {
                if (drawer.IsDrawerOpen(GravityCompat.Start))
                {
                    drawer.CloseDrawer(GravityCompat.Start);
                }
                else
                {
                    base.OnBackPressed();
                }
            }
            else
            {
                SetBaseView();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_admin_logout)
            {
                Preferences.Set("my_key", 0);
                StartActivity(typeof(LoginActivity));
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            string myPref = Preferences.Get("default_view", "default");
            if (myPref == "topics")
            {
                StartActivity(typeof(AddTopicActivity));
            }
            else if (myPref == "quizzes")
            {
                StartActivity(typeof(AddQuizActivity));
            }
            else if (myPref == "questions")
            {
                StartActivity(typeof(AddQuestionActivity));
            }
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_topic)
            {
                Preferences.Set("default_view", "topics");
                MainListSettings();
            }
            else if (id == Resource.Id.nav_quiz)
            {
                Preferences.Set("default_view", "quizzes");
                MainListSettings();
            }
            else if (id == Resource.Id.nav_question)
            {
                Preferences.Set("default_view", "questions");
                MainListSettings();
            }
            else if (id == Resource.Id.nav_see_requests)
            {
                Preferences.Set("default_view", "requests");
                MainListSettings();
            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}