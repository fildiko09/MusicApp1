using System;
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
using Java.Nio.Channels;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using Microsoft.VisualStudio.Services.WebApi;
using MusicApp1.Models.Entities;
using MusicApp1.ViewModels;
using MusicApp1.Views;
using SQLite;
using Xamarin.Essentials;

namespace MusicApp1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        UserVM userVM;
        TopicTable[] artists;
        TopicTable[] histories;
        ListView topicList;
        string[] items;
        QuizTable[] quizzes;
        string[] items1;
        public MainActivity()
        {
            userVM = new UserVM();
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            artists = userVM.GetTopicsInCategory("artist");
            histories = userVM.GetTopicsInCategory("history");

            try
            {
                int id = Preferences.Get("my_key", 0);
                var user = userVM.GetUserById(id);
                Toast.MakeText(this, user.email.ToString(), ToastLength.Long).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }

            SetUserView();
        }

        public void SetUserView()
        {
            SetContentView(Resource.Layout.activity_main);
            AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar1);
            SetSupportActionBar(toolbar);

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout1);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav1_view);
            navigationView.SetNavigationItemSelectedListener(this);

            UserListSettings();
        }


        private void UserListSettings()
        {
            string myPref = Preferences.Get("user_view", "default");
            if (myPref == "history" || myPref == "artist")
            {
                if (myPref == "history")
                {
                    items = (from o in histories select o.topicName).ToArray();
                }
                else if (myPref == "artist")
                {
                    items = (from o in artists select o.topicName).ToArray();
                }

                topicList = (ListView)FindViewById<ListView>(Resource.Id.filteredListView);
                topicList.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, items);

                topicList.ItemClick += ItemOnClick;
            }
            else if (myPref=="request")
            {
                StartActivity(typeof(AddRequestActivity));
            }
        }

        string title;
        string description;
        TopicTable selectItem;
        private void ItemOnClick(object sender, AdapterView.ItemClickEventArgs eventArgs)
        {
            string myPref = Preferences.Get("user_view", "default");
            if (myPref == "history" || myPref == "artist")
            {
                SetContentView(Resource.Layout.Information);
                if (myPref == "history")
                {
                    selectItem = histories[eventArgs.Position];
                }
                else if (myPref == "artist")
                {
                    selectItem = artists[eventArgs.Position];
                }

                TextView txtTitle = FindViewById<TextView>(Resource.Id.TopicSeeName);
                txtTitle.Text = selectItem.topicName;

                TextView txtDesc = FindViewById<TextView>(Resource.Id.TopicSeeDescription);
                txtDesc.Text = selectItem.description;

                Spinner spinner = FindViewById<Spinner>(Resource.Id.chooseQuizFK);

                TextView choose = FindViewById<TextView>(Resource.Id.ChoseQuiz);
                var btnGo = FindViewById<Button>(Resource.Id.button_go);

                quizzes = userVM.GetQuizzesInTopic(selectItem.topicId);
                items1 = (from o in quizzes select o.quizName).ToArray();

                if (quizzes.Length == 0)
                {
                    spinner.Visibility = ViewStates.Invisible;
                    btnGo.Visibility = ViewStates.Gone;
                    choose.Visibility = ViewStates.Invisible;
                }
                else
                {
                    spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);

                    var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, items1);
                    adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                    spinner.Adapter = adapter;

                    iter = 0;
                    score = 0;

                    btnGo.Click += go_Click;
                }
            }
            else if (myPref == "request")
            {
                StartActivity(typeof(AddRequestActivity));
            }
        }

        int idQuiz;
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            idQuiz = quizzes[e.Position].quizId;
        }

        QuestionTable[] questionsInQuiz;
        string selectAnswer;
        int score;

        int iter;
        private void go_Click(object sender, EventArgs e)
        {
            try
            {
                SetContentView(Resource.Layout.PlayQuiz);

                if (iter == 0)
                {
                    questionsInQuiz = userVM.GetQuestionsInQuiz(idQuiz);
                    questionsInQuiz = Randomize2(questionsInQuiz);
                }
                TextView question = FindViewById<TextView>(Resource.Id.QuestionSee);

                Button next_btn = FindViewById<Button>(Resource.Id.submitAnsw);

                GridView grid = FindViewById<GridView>(Resource.Id.answer);

                question.Text = questionsInQuiz[iter].question;
                string[] answeres = { questionsInQuiz[iter].answerA, questionsInQuiz[iter].answerB, questionsInQuiz[iter].answerC, questionsInQuiz[iter].correctAnswer };

                answeres = Randomize(answeres);

                var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSelectableListItem, answeres);
                grid.Adapter = adapter;

                grid.ItemClick += (sender, e) => { selectAnswer = answeres[e.Position]; };

                next_btn.Click += NextBtn_Click;

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
        }

        private static string[] Randomize(string[] ans)
        {
            var randomAns = from a in ans orderby Guid.NewGuid() ascending select a;
            return randomAns.ToArray();
        }

        private static QuestionTable[] Randomize2(QuestionTable[] que)
        {
            var randomQue = from a in que orderby Guid.NewGuid() ascending select a;
            return randomQue.ToArray();
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
                if(selectAnswer == questionsInQuiz[iter].correctAnswer)
                {
                    score += 1;
                    iter += 1;
                    Toast.MakeText(this, "correct", ToastLength.Long).Show();
                    if (iter < questionsInQuiz.Length)
                    {
                        go_Click(sender, e);
                    }
                    else 
                    {
                        try
                        {
                            SetContentView(Resource.Layout.Result);
                            TextView res = FindViewById<TextView>(Resource.Id.ResultSee);
                            res.Text = "Your score is " + score + "!";
                        }
                        catch (Exception ex)
                        {
                            Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
                        }
                    }
                }
                else
                {
                    iter += 1;
                    Toast.MakeText(this, "incorrect", ToastLength.Long).Show();
                    if (iter < questionsInQuiz.Length)
                    {
                        go_Click(sender, e);
                    }
                    else
                    {
                        try
                        {
                            SetContentView(Resource.Layout.Result);
                            TextView res = FindViewById<TextView>(Resource.Id.ResultSee);
                            res.Text = "Your score is " + score + "!";
                        }
                        catch (Exception ex)
                        {
                            Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
                        }
                    }

                }
        }

        [Obsolete]
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
                    UserListSettings();
                }

                SetUserView();
            }
            else
            {
                SetUserView();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_user, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action1_logout)
            {
                Preferences.Set("my_key", 0);
                StartActivity(typeof(LoginActivity));
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_history)
            {
                Preferences.Set("user_view", "history");
            }
            else if (id == Resource.Id.nav_artist)
            {
                Preferences.Set("user_view", "artist");
            }
            else if (id == Resource.Id.nav_request)
            {
                Preferences.Set("user_view", "request");
            }

            UserListSettings();
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout1);
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

