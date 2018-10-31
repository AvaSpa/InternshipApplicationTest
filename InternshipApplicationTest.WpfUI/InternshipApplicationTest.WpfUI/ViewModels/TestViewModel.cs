using Caliburn.Micro;
using InternshipApplicationTest.Common.ModelClasses;
using InternshipApplicationTest.Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Threading;

namespace InternshipApplicationTest.WpfUI.ViewModels
{
    public class TestViewModel : Screen
    {
        private TestQuestionModel _currentQuestion;
        private TestAnswerModel _currentQuestionSelectedAnswer;
        private TimeSpan _timeLeft;

        private List<TestQuestionModel> questions;
        private List<TestAnswerModel> answers;
        private TestModel test;
        private short score;
        private Timer timer;

        public TestViewModel(TestModel test)
        {
            this.test = test;
            questions = test.Questions;
            timer = new Timer(Timer_Tick, null, 500, 1000);
            TimeLeft = new TimeSpan(0, test.TimeLimitInMinutes, 0);
            CurrentQuestionAnswers = new BindableCollection<TestAnswerModel>();
        }

        private void Timer_Tick(object state)
        {
            if (TimeLeft > new TimeSpan())
            {
                TimeLeft -= new TimeSpan(0, 0, 1);
            }
            else
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);
                App.Current.Dispatcher.Invoke((System.Action)(() => { EndTest(score); }));
            }
        }

        public TimeSpan TimeLeft
        {
            get
            {
                return _timeLeft;
            }
            set
            {
                _timeLeft = value;
                NotifyOfPropertyChange(() => TimeLeft);
            }
        }

        public BindableCollection<TestAnswerModel> CurrentQuestionAnswers { get; set; }

        public TestQuestionModel CurrentQuestion
        {
            get
            {
                return _currentQuestion;
            }
            set
            {
                _currentQuestion = value;
                NotifyOfPropertyChange(() => CurrentQuestion);
                NotifyOfPropertyChange(() => CanPostponeQuestion);
                CurrentQuestionAnswers.Clear();
                CurrentQuestionAnswers.AddRange(GetAnswersForQuestion(CurrentQuestion.Id));
            }
        }

        public bool CanPostponeQuestion
        {
            get
            {
                return questions.Count > 1;
            }
        }

        private List<TestAnswerModel> GetAnswersForQuestion(int id)
        {
            return this.answers.Where(a => a.QuestionId == id).ToList();
        }

        public TestAnswerModel CurrentQuestionSelectedAnswer
        {
            get
            {
                return _currentQuestionSelectedAnswer;
            }
            set
            {
                _currentQuestionSelectedAnswer = value;
                NotifyOfPropertyChange(() => CurrentQuestionSelectedAnswer);
            }
        }

        protected override async void OnViewLoaded(object view)
        {
            var answers = await DataAccess.GetAnswers();
            this.answers = answers.Where(a => questions.Select(q => q.Id).Contains(a.QuestionId)).ToList();
            CurrentQuestion = questions[0];
        }

        public void AnswerQuestion()
        {
            if (CurrentQuestionSelectedAnswer != null)
            {
                if (CurrentQuestionSelectedAnswer.Id == CurrentQuestion.CorrectAnswerId)
                {
                    score++;
                }

                questions.Remove(CurrentQuestion);
                if (!DisplayNextQuestion())
                {
                    EndTest(score);
                }
            }
        }

        public void PostponeQuestion()
        {
            if (questions.Count > 0)
            {
                var question = questions[0];
                questions.Remove(question);
                questions.Add(question);
            }
            DisplayNextQuestion();
        }

        private bool DisplayNextQuestion()
        {
            if (questions.Count > 0)
            {
                CurrentQuestion = questions[0];
                return true;
            }
            return false;
        }

        private void EndTest(short score)
        {
            var shell = (ShellViewModel)Parent;
            shell.EndTest(test.ApplicantInternshipId, score);
        }
    }
}
