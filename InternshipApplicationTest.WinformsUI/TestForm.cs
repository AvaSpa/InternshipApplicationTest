using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using InternshipApplicationTest.Common.ModelClasses;

namespace InternshipApplicationTest.WinformsUI
{
    public partial class TestForm : Form
    {
        private string baseUrl;
        private TestModel test;
        private List<TestAnswerModel> answers;
        private TestQuestionModel currentQuestion;

        public TestForm()
        {
            InitializeComponent();
        }

        public TestForm(string baseUrl, TestModel test) : this()
        {
            this.baseUrl = baseUrl;
            this.test = test;
        }

        private async void TestForm_Load(object sender, EventArgs e)
        {
            await LoadAnswers();

            StartTimer();

            DisplayNextQuestion();
        }

        private void StartTimer()
        {
            var t = new Thread(ThreadJob);
            t.Start();
        }

        private void ThreadJob()
        {
            var time = new TimeSpan(0, test.TimeLimitInMinutes, 0);
            while (time > new TimeSpan())
            {
                ThreadSafeSetLabelText(lblTime, time.ToString(@"mm\:ss"));
                time -= new TimeSpan(0, 0, 1);
                Thread.Sleep(1000);
            }
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(this.Close));
            }
            else
            {
                this.Close();
            }
        }

        private void ThreadSafeSetLabelText(Label lbl, string value)
        {
            if (lbl.InvokeRequired)
            {
                lbl.Invoke(new Action(() => { lbl.Text = value; }));
            }
            else
            {
                lbl.Text = value;
            }
        }

        /// <summary>
        /// Displays the next question.
        /// </summary>
        /// <returns>True if there are questions left, false otherwise</returns>
        private bool DisplayNextQuestion()
        {
            var questions = test.Questions;
            if (questions.Count > 0)
            {
                currentQuestion = questions[0];
                lblQuestion.Text = currentQuestion.Statement;

                var questionAnswers = answers.Where(a => a.QuestionId == currentQuestion.Id).ToList();
                lbAnswers.Top = lblQuestion.Top + lblQuestion.Height + 10;
                lbAnswers.DataSource = questionAnswers;
                lbAnswers.SelectedIndex = -1;

                return true;
            }

            return false;
        }

        private String Number2String(int number, bool isCaps)
        {
            Char c = (Char)((isCaps ? 65 : 97) + (number - 1));
            return c.ToString();
        }

        private void LbAnswers_Format(object sender, ListControlConvertEventArgs e)
        {
            var answer = (TestAnswerModel)e.ListItem;
            var answerIndex = lbAnswers.Items.IndexOf(answer);
            e.Value = $"{Number2String(answerIndex + 1, true)}. {answer.Content}";
        }

        private async Task LoadAnswers()
        {
            var webclient = new WebClient<TestAnswerModel>(baseUrl);
            var results = await webclient.GetAsync<List<TestAnswerModel>>("");
            answers = results.Where(a => test.Questions.Select(q => q.Id).Contains(a.QuestionId)).ToList();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (lbAnswers.SelectedIndex >= 0)
            {
                var currentQuestionAnswer = (TestAnswerModel)lbAnswers.SelectedItem;
                if (currentQuestionAnswer.Id == currentQuestion.CorrectAnswerId)
                {
                    ScoreCalculator.Instance.Score++;
                }

                test.Questions.Remove(currentQuestion);
                if (!DisplayNextQuestion())
                {
                    this.Close();
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (test.Questions.Count > 0)
            {
                var question = test.Questions[0];
                test.Questions.Remove(question);
                test.Questions.Add(question);
            }
            DisplayNextQuestion();
        }
    }
}
