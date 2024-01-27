using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.Android.Material.FloatingActionButton;
using MusicApp1.Models.Entities;
using MusicApp1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicApp1.Views
{
    [Activity(Label = "AddQuestion")]
    public class AddQuestionActivity : Activity
    {
        EditText txtQuestion;
        EditText txtAnswerA;
        EditText txtAnswerB;
        EditText txtAnswerC;
        EditText txtCorrectAnswer;
        AddQuestionVM addQuestionVM;
        int idQuiz;
        QuizTable[] quizzes;
        string[] items;

        public AddQuestionActivity()
        {
            addQuestionVM = new AddQuestionVM();
        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AddQuestion);

            var btnSave = FindViewById<FloatingActionButton>(Resource.Id.btnSaveQuestion);
            txtQuestion = FindViewById<EditText>(Resource.Id.addYourQuestion);
            txtAnswerA = FindViewById<EditText>(Resource.Id.addAnswerA);
            txtAnswerB = FindViewById<EditText>(Resource.Id.addAnswerB);
            txtAnswerC = FindViewById<EditText>(Resource.Id.addAnswerC);
            txtCorrectAnswer = FindViewById<EditText>(Resource.Id.addCorrectAnswer);
            Spinner spinner = FindViewById<Spinner>(Resource.Id.selectQuiz);

            quizzes = addQuestionVM.GetAllQuizzes();
            items = (from o in quizzes select o.quizName).ToArray();

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);

            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, items);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
            btnSave.Click += save_Click;
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            idQuiz = quizzes[e.Position].quizId;
        }

        private void save_Click(object sender, EventArgs e)
        {
            try
            {
                QuestionTable table = new QuestionTable();
                table.question = txtQuestion.Text;
                table.answerA = txtAnswerA.Text;
                table.answerB = txtAnswerB.Text;
                table.answerC = txtAnswerC.Text;
                table.correctAnswer = txtCorrectAnswer.Text;
                table.quizIdFK = idQuiz;
                addQuestionVM.AddQuestion(table);
                Toast.MakeText(this, "Question Added…", ToastLength.Long).Show();
                StartActivity(typeof(AdminActivity));
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
        }
    }
}